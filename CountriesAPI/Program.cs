using CountriesAPI.HttpClients;
using CountriesAPI.Interfaces.HttpClients;
using CountriesAPI.Interfaces.Services;
using CountriesAPI.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<ICountriesHttpClient, CountriesHttpClient>();
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddSwaggerGen();

// Configure Serilog to log to the console
var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("CountriesAPI", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
