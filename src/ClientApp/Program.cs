using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ClientApp.Data;
using ClientApp;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddHttpClient<ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5050/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "CfDJ8JhqidJmAh5Hg8yMIJzNjljPnaMPNohSe33ePuDtsf4GTdrlThnYKJGTi2IRoRCmfxz7xyL9VKbscDYg3UBuxBgf8WhG9lVs8EKmJMhmacnEB6OXqd6NN16uTAJqtCADfJr1Qay6wxpBgmPEatMTrsvYPagNqZ3t288jm4pHNSVvxaSiyNSg8ixSi8XTWVGIDK17dLou1mSrrnlsqNQxNepI8JVQwjyent-xnnG0c-EcmWx1JxNTrfbB_r12WVB1838WMnaXmYn0PJidu5QKYqomfROWI6gJnyGoi-v7ux4ND8Vm9eCoQXB8SKpVmThXs4OEaM-0B7UTH3pVKNFYNt3ntfCNrmFBy6OsrVmw7_QAtRyeq9XMCY-v7FfyhFnBfQL9oV03H9EtEmOOHbb22DBBv7rZILtE4Ilk4tc3bd3thD-FQoj5zoMlToR8vplJ6711teuIv-sPHl2VELlVVGmzROss2IS96h99aeFYXMzL_K_feOiR6BUk9fIMzPpZnLaknKr1-aeYirgue_2ICyxq_eqzjbPvhaQaGX-m0GIRIzBWKBuRnCz0jLEfl-JIlrHetc7b74uRjYlDUyht0V8Q_EU5XuS6CXmcjqu9wZ9m4sIhWkSmcw5VblqkqaEKjmv-4scGrp0o-voSTEjWphIuJ3YjgazUeCf2qS4ipH3OJpgVv_ZqNh07DmqUDUQm3g");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
