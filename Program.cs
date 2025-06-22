using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using VendingMachineApi;
using VendingMachineApi.Endpoints;
using VendingMachineApi.Extensions;
using VendingMachineApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECT");
if (connectionString == null)
{
    throw new Exception("Connection string to Database was null");
}
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});
builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.RegisterServices();

var app = builder.Build();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt
        .WithBaseServerUrl("/scalar")
        .WithTitle("Vending machine api")
        .WithTheme(ScalarTheme.BluePlanet)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}
app.UseHealthChecks("/healtz");

app.UseBrandEndpoints(); //Products!!!!! Cycle on get products
app.UseProductEndpoints();

using (var scope = app.Services.CreateScope())
{
    if (!scope.ServiceProvider.GetService<AppDbContext>()!.Database.CanConnect())
        throw new Exception("Connection to the Db cannot be established");
    var report = await scope.ServiceProvider.GetService<HealthCheckService>()!.CheckHealthAsync();
    var postgres = report.Entries.FirstOrDefault(e => e.Key.Contains("AppDbContext", StringComparison.OrdinalIgnoreCase)).Value;
    if (postgres.Status != HealthStatus.Healthy)
        throw new Exception("Db is unhealthy");
}
app.Run();