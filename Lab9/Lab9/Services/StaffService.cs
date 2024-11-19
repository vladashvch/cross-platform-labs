using System.Net.Http.Json;
using DotNetEnv;
using Lab9.Models;

namespace Lab9.Services
{
    public class StaffService
    {
        private readonly HttpClient _httpClient;

        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Staff>> GetStaffAsync()
        {
            var apiBaseUrl = "https://localhost:7125/lab6/staff";
            var response = await _httpClient.GetFromJsonAsync<List<Staff>>(apiBaseUrl);
            return response ?? new List<Staff>();
        }

        public async Task<bool> CreateStaffAsync(Staff staff)
        {
            var apiBaseUrl = "https://localhost:7125/lab6/staff";
            var response = await _httpClient.PostAsJsonAsync(apiBaseUrl, staff);

            return response.IsSuccessStatusCode;
        }
    }
}