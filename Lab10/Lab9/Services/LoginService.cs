using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DotNetEnv;
using Lab9.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace Lab9.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public class TokenResponse
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }

        public class LoginRequest
        {
            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; }
        }

        public async Task<string> Login(string username, string password)
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };
            var apiBaseUrl = "https://localhost:7096/auth/login";

            var response = await _httpClient.PostAsJsonAsync(apiBaseUrl, loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return result.Token;
            }
            else
            {
                return null;
            }
        }
    }
}