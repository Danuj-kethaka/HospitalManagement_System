using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Hospital.FrontEnd.Clients;

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

    public async Task<List<Appointment>> GetAllAppointmentAsync(string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",jwtToken);
        return await _httpClient.GetFromJsonAsync<List<Appointment>>("appointments/all") ??
        new List<Appointment>();
    }
}