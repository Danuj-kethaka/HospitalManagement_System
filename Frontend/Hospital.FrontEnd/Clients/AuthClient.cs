using Hospital.FrontEnd.Models;

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
}