using System.Net.Http.Json;
using System.Text.Json;
using DotNetEnv;
using Lab9.Models;
using Newtonsoft.Json;

namespace Lab9.Services
{
    public class PatientMedicationService
    {
        private readonly HttpClient _httpClient;

        public PatientMedicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PatientMedication>> GetPatientMedicationsAsync()
        {
            var apiBaseUrl = "https://localhost:7125/lab6/patient-medications";
            var response = await _httpClient.GetFromJsonAsync<List<PatientMedication>>(apiBaseUrl);
            return response ?? new List<PatientMedication>();
        }
    }
}