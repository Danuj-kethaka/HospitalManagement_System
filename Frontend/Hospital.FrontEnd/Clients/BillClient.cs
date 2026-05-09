using System.Net.Http.Headers;
using System.Net.Http.Json;
using Hospital.FrontEnd.Models;

namespace Hospital.FrontEnd.Clients;

public class BillClient
{
    private readonly HttpClient _httpClient;

    public BillClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // USER: get own bills
    public async Task<List<Bill>?> GetMyBillsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<Bill>>("bills");
    }

    // ADMIN: create bill
    public async Task<HttpResponseMessage> CreateBillAsync(Bill bill, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.PostAsJsonAsync("bills", bill);
    }

    // ADMIN: get all bills
    public async Task<List<Bill>?> GetAllBillsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<List<Bill>>("bills/all");
    }
}