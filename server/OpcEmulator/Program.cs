// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;


#region Infrastructure
var builder = Host.CreateApplicationBuilder(args);
var baseAddress = Environment.GetEnvironmentVariable("BaseAddress");
var tempTimeout = Convert.ToInt32(Environment.GetEnvironmentVariable("Timeout"));
if (string.IsNullOrEmpty(baseAddress))
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: false);
    var configuration = builder.Configuration;
    baseAddress = configuration.GetValue<string>("BaseAddress") ?? "http://localhost:5034";
    tempTimeout = configuration.GetValue<int>("Timeout");
}

var baseUri = new Uri(baseAddress);
var timeout = tempTimeout > 0 ? tempTimeout : 5;
builder.Services.AddHttpClient<HttpService>(client =>
    { 
        client.BaseAddress = new Uri(baseAddress);
        client.Timeout = TimeSpan.FromSeconds(timeout);
    });
builder.Services.AddHostedService<OpcWorker>();

#endregion

Console.WriteLine("Running OpcEmulator...");
var app = builder.Build();
Console.WriteLine("For exit please press Enter");
await app.RunAsync();


#region Services
internal class HttpService(HttpClient httpClient)
{
    private readonly HttpClient httpClient  = httpClient;

    public async Task SendAsync(int _sesnorId, float _value)
    {
        await httpClient.PostAsJsonAsync("api/data", new {
            sensorId = _sesnorId, 
            value = _value
            }
        );
    }
}

internal class OpcWorker(HttpService httpService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested) {
            for (int i = 1; i < 4; i++) {
                try {
                    await httpService.SendAsync(i, Random.Shared.Next(0, 101));
                } catch { }
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}

#endregion