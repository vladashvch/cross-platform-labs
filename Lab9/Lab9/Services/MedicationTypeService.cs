using System.Net.Http.Json;
using DotNetEnv;
using Lab9.Models;

namespace Lab9.Services
{
    public class MedicationTypeService
    {
        private readonly HttpClient _httpClient;

        public MedicationTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MedicationType>> GetMedicationTypesAsync()
        {
            var apiBaseUrl = "https://localhost:7125/lab6/v2/medication-types";
            var response = await _httpClient.GetFromJsonAsync<List<MedicationType>>(apiBaseUrl);
            return response ?? new List<MedicationType>();
        }
    }
}