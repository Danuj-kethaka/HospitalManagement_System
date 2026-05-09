using System.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Hospital.FrontEnd.Clients;
using Hospital.FrontEnd.Models;
using System.Text.Json;


public class AppointmentClient
{
    private readonly HttpClient _httpClient;

    public AppointmentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> CreateAppointmentAsync(CreateAppointment appointment, string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        return await _httpClient.PostAsJsonAsync("appointments", appointment);
    }

    public async Task<List<Appointment>> GetMyAppointmentsAsync(string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        return await _httpClient.GetFromJsonAsync<List<Appointment>>("appointments")  ??
         new List<Appointment>();
    }

    public async Task<List<Appointment>> GetAllAppointmentsAsync(string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        return await _httpClient.GetFromJsonAsync<List<Appointment>>("appointments/all") ?? new List<Appointment>();
    }

    public async Task<HttpResponseMessage> UpdateAppointmentStatusAsync(int id, string status, string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        var request = new { status };
        return await _httpClient.PutAsJsonAsync($"appointments/{id}/status", request);
    }

   
}