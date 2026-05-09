using Hospital.FrontEnd.Clients;
using Hospital.FrontEnd.Components;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


builder.Services.AddScoped<AuthClient>();
builder.Services.AddScoped<AppointmentClient>();
builder.Services.AddScoped<MedicalRecordClient>();
builder.Services.AddScoped<BillClient>();
builder.Services.AddScoped<LabResultClient>();


var HospitalApiUrl = builder.Configuration["HospitalApiUrl"] ?? throw new Exception("HospitalApiUrl is not set");
builder.Services.AddHttpClient<AuthClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));
builder.Services.AddHttpClient<AppointmentClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));
builder.Services.AddHttpClient<MedicalRecordClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));
builder.Services.AddHttpClient<BillClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));
builder.Services.AddHttpClient<LabResultClient>(client => client.BaseAddress = new Uri(HospitalApiUrl));

builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<Radzen.ContextMenuService>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

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
