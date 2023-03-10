using PowerBoss.Infra.Serial.Solar.Interfaces;
using PowerBoss.Infra.Serial.Solar.Models;

namespace PowerBoss.Worker;

public class InverterService : BackgroundService
{
    private readonly ILogger<InverterService> _logger;
    private readonly IInverter _inverter;

    public InverterService(
        ILogger<InverterService> logger,
        IInverter inverter
    )
    {
        _logger = logger;
        _inverter = inverter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Generation data = await _inverter.Get<Generation>();
            // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _inverter.Connect();

        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _inverter.Disconnect();

        await base.StopAsync(cancellationToken);
    }
}