using DotNetEnv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Lab6.Controllers
{
    public class Auth0Service
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _auth0Domain = Env.GetString("DOMAIN");
        private readonly string _clientId = Env.GetString("CLIENT_ID");
        private readonly string _clientSecret = Env.GetString("CLIENT_SECRET");
        private readonly string _audience = $"https://{_auth0Domain}/api/v2/";

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
                    return false;
                }

                return true;
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
