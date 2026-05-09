using System.Net.Http.Headers;
using System.Net.Http.Json;
using Hospital.FrontEnd.Models;

namespace Hospital.FrontEnd.Clients;

public class MedicalRecordClient
{
    private readonly HttpClient _httpClient;

    public MedicalRecordClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MedicalRecord>?> GetMyMedicalRecordsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<MedicalRecord>>("medical-records");
    }

    public async Task<HttpResponseMessage> CreateMedicalRecordAsync(MedicalRecord record, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.PostAsJsonAsync("medical-records", record);
    }

    public async Task<List<MedicalRecord>?> GetAllMedicalRecordsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<MedicalRecord>>("medical-records/all");
    }
}