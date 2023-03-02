using PowerBoss.Domain.Interfaces;
using PowerBoss.Domain.Models;
using PowerBoss.Infra.Api.Tesla.Models;
using PowerBoss.Worker.Extensions;

namespace PowerBoss.Worker;

public class Worker : BackgroundService
{
    private readonly TeslaClient _client;
    private readonly ITeslaVehicleRepository _repository;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, TeslaClient client, ITeslaVehicleRepository repository)
    {
        _logger = logger;
        _client = client;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        // _repository.GetCollection();
        
        // await _client.RefreshToken(stoppingToken);
        IEnumerable<Vehicle> vehicles = await _client.GetVehicles(stoppingToken);

        foreach (Vehicle vehicle in vehicles)
        {
            await _repository.InsertOne(new VehicleModel()
            {
                Name = vehicle.DisplayName
            }, stoppingToken);
            // VehicleChargeState? wake = await _client.CommandWake(vehicle, stoppingToken);
            // VehicleChargeState? chargeState = await _client.GetVehicleChargingState(vehicle, stoppingToken);
            // VehicleDriveState? driveState = await _client.GetVehicleDriveState(vehicle, stoppingToken);
            // VehicleGuiSettings? guiSettings = await _client.GetVehicleGuiSettings(vehicle, stoppingToken);
            // VehicleState? vehicleState = await _client.GetVehicleState(vehicle, stoppingToken);
            // CommandResponse? commandFlashLights = await _client.CommandLightFlash(vehicle, stoppingToken);
            // await Task.Delay(1000, stoppingToken);
            // CommandResponse? chargePortOpenState = await _client.CommandChargePortOpen(vehicle, stoppingToken);
            // await Task.Delay(2000, stoppingToken);
            // CommandResponse? chargePortCloseState = await _client.CommandChargePortClose(vehicle, stoppingToken);
            // await Task.Delay(1000, stoppingToken);
            // await _client.CommandLightFlash(vehicle, stoppingToken);
            // await _client.SetChargeLimit(vehicle, 80, stoppingToken);
            // await _client.SetChargingAmps(vehicle, 80, stoppingToken);
            await _client.SetScheduledCharging(vehicle, new TimeOnly(13, 37), stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}