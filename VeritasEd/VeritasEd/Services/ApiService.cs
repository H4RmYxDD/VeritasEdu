using System.Net.Http.Json;
using VeritasEd.Api.Models;
using VeritasEd.Models;

namespace VeritasEd.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5272/api";

        public ApiService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            });
        }

        public async Task<bool> RegisterAsync(Api.Models.User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/User/register", user);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Registration failed: {response.StatusCode} - {error}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<Api.Models.User?> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/User/login", new Api.Models.User { Username = username, Password = password });
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Api.Models.User>();
            return null;
        }
        public async Task<List<Grade>> GetGradesByUserIdAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Grade>>($"{BaseUrl}/Grades/{userId}") ?? new List<Grade>();
        }

        public async Task<bool> AddGradeAsync(Grade grade)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Grades", grade);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Grade>>($"{BaseUrl}/Grades") ?? new List<Grade>();
        }
        public async Task<bool> DeleteGradeAsync(int gradeId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/Grades/{gradeId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/Grades/{grade.Id}", grade);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Api.Models.User>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Api.Models.User>>($"{BaseUrl}/User/students") ?? new List<Api.Models.User>();
        }
    }
}