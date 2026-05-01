using System.Net.Http.Json;
using Hospital.FrontEnd.Models;
using Microsoft.AspNetCore.Components;

public class AuthClient
{
    private readonly HttpClient httpClient;

    public AuthClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> AddUser(UserDetails user)
    {
        return await httpClient.PostAsJsonAsync("register", user);
    }

    public async Task<HttpResponseMessage> Login (LoginDetails login)
    {
        return await httpClient.PostAsJsonAsync("login",login);
        
    }

    public async Task<UserDetails?> GetCurrentUser(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync("users/me");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"ERROR: {response.StatusCode}");
            return null;
        }

        var data = await response.Content.ReadFromJsonAsync<UserDetails>();
        return data;
    }

    public async Task<List<UserDetails>> GetAllUsers(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return await httpClient.GetFromJsonAsync<List<UserDetails>>("users/all")
            ?? new List<UserDetails>();
    }

}