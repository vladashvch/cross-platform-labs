using DotNetEnv;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Lab5.Controllers
{
    public class Auth0Service
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _auth0Domain = Env.GetString("DOMAIN");
        private readonly string _clientId = Env.GetString("CLIENT_ID");
        private readonly string _clientSecret = Env.GetString("CLIENT_SECRET");
        private readonly string _audience = $"https://{_auth0Domain}/api/v2/";

        public async Task<string> GetClientToken()
        {
            try
            {
                var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("audience", _audience),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret)
                });

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/oauth/token")
                {
                    Content = requestData
                };

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error getting client token: {response.StatusCode} - {errorResponse}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
                return tokenResponse.access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetClientToken: {ex.Message}");
                throw;
            }
        }

        public async Task CreateUser(string username, string fullname, string password, string phone, string email, string clientToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/api/v2/users");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

            var jsonContent = new
            {
                email = email,
                name = fullname,
                nickname = username,
                user_id = Guid.NewGuid().ToString(),
                connection = "Username-Password-Authentication",
                password = password,
                verify_email = false,
                user_metadata = new { phone = phone }
            };

            request.Content = new StringContent(
                JsonConvert.SerializeObject(jsonContent),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating user: {errorResponse}");
            }
        }

        public async Task<string> GetUserToken(string username, string password)
        {
            var requestData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("audience", _audience),
                new KeyValuePair<string, string>("scope", "openid profile offline_access"),
                new KeyValuePair<string, string>("connection", "Username-Password-Authentication")
            });

            HttpResponseMessage response = await _httpClient.PostAsync($"https://{_auth0Domain}/oauth/token", requestData);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error getting user token: {errorResponse}");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
            return tokenResponse.access_token;
        }
        public async Task<dynamic> GetUser(string accessToken)
        {
            var userId = await GetUserId(accessToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID could not be retrieved from the token.");
            }

            var options = new HttpRequestMessage(HttpMethod.Get, $"https://{_auth0Domain}/api/v2/users/{userId}");
            options.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _httpClient.SendAsync(options);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Request failed with status code: {response.StatusCode}, Content: {content}");
                }

                var user = JsonConvert.DeserializeObject<JObject>(content);
                return new
                {
                    nickname = user["nickname"]?.ToString(),
                    name = user["name"]?.ToString(),
                    email = user["email"]?.ToString(),
                    picture = user["picture"]?.ToString(),
                    phone = user["user_metadata"]?["phone"]?.ToString(),
                };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("getUser: Invalid credentials or request failed", ex);
            }
        }

        public async Task<string> GetUserId(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = jwtHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in token.");
                }

                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to decode JWT or extract user ID.", ex);
            }
        }
    }
}