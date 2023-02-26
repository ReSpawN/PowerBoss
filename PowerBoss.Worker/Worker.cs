using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Worker;

public class Worker : BackgroundService
{
    private readonly TeslaClient _client;
    private readonly ILogger<Worker> _logger;

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
            // VehicleDriveState? driveState = await _client.GetVehicleDriveState(vehicle, stoppingToken);
            // VehicleGuiSettings? guiSettings = await _client.GetVehicleGuiSettings(vehicle, stoppingToken);
            VehicleState? vehicelState = await _client.GetVehicleState(vehicle, stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}