using System.Net.Http.Headers;
using System.Net.Http.Json;
using Hospital.FrontEnd.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Hospital.FrontEnd.Clients;

public class LabResultClient
{
    private readonly HttpClient _httpClient;

    public LabResultClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // USER GET OWN LAB RESULTS
    public async Task<List<LabResult>?> GetMyLabResultsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<LabResult>>("labresults");
    }

    // ADMIN GET ALL LAB RESULTS
    public async Task<List<LabResult>?> GetAllLabResultsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<LabResult>>("labresults/all");
    }

    // ADMIN CREATE LAB RESULT
    public async Task<HttpResponseMessage> CreateLabResultAsync(
        string token,
        string userId,
        string title,
        string resultText,
        IBrowserFile? file)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        using var content = new MultipartFormDataContent();

        content.Add(
            new StringContent(userId),
            "UserId");

        content.Add(
            new StringContent(title),
            "Title");

        content.Add(
            new StringContent(resultText),
            "ResultText");

        if (file is not null)
        {
            var stream = file.OpenReadStream(10 * 1024 * 1024);

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            content.Add(
                fileContent,
                "File",
                file.Name);
        }

        return await _httpClient.PostAsync(
            "labresults",
            content);
    }
}