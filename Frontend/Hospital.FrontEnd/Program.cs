using Hospital.FrontEnd.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
//var hospitalmanagementApiUrl = builder.configuration["HospitalManagementApiUrl"] ?? throw new Exception("HospitalManagementApiUrl is not set");
//builder.services.AddHttpClient<HospitalClient>(hospital => hospital.BaseAddress = new Uri(hospitalmanagementApiUrl));

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

app.MapRazorComponents<App>();

app.Run();
