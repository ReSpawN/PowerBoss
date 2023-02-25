using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.WebUtilities;
using PowerBoss.Domain.Models;
using PowerBoss.Domain.Models.Vehicle;
using TeslaAuth;

namespace PowerBoss.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly TeslaClient _client;
    private readonly IHttpClientFactory _clientFactory;
    public Worker(ILogger<Worker> logger, TeslaClient client)
    {
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IEnumerable<VehicleSynopsis> vehicles = await _client.GetVehicles(stoppingToken);

        foreach (VehicleSynopsis vehicle in vehicles)
        {
            // VehicleChargeState? chargeState = await _client.GetVehicleChargingState(vehicle, stoppingToken);
            VehicleChargeState? driveState = await _client.GetVehicleDriveState(vehicle, stoppingToken);
        }
        
       

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}