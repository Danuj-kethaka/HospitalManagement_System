using Hospital.FrontEnd.Clients;
using Hospital.FrontEnd.Components;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<AuthClient>();
// In Program.cs (Frontend)
builder.Services.AddScoped<AppointmentClient>();
var HospitalApiUrl = builder.Configuration["HospitalApiUrl"] ?? throw new Exception("HospitalApiUrl is not set");
builder.Services.AddHttpClient<AuthClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));
builder.Services.AddHttpClient<AppointmentClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));

builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<Radzen.ContextMenuService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
