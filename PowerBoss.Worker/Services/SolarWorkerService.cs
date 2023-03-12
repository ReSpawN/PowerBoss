using System.Diagnostics;
using PowerBoss.Domain.Solar.Interfaces;

namespace PowerBoss.Worker.Services;

public class SolarWorkerService : BackgroundService
{
    private const int Interval = 1000;

    private readonly ILogger<SolarWorkerService> _logger;
    private readonly ISolarService _service;

    public SolarWorkerService(
        ILogger<SolarWorkerService> logger,
        ISolarService service
    )
    {
        _logger = logger;
        _service = service;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _service.Connect();
        _service.OnDisconnect(() => base.StopAsync(cancellationToken));

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Stopwatch stopwatch = new();
        while (!stoppingToken.IsCancellationRequested)
        {
            stopwatch.Start();
            await _service.LogRegisterInterval();
            stopwatch.Stop();

            // InverterRegister data = await _inverter.Read<InverterRegister>();
            // _logger.LogInformation($"Raw power produced {data.GenerationRegister.DcPowerInWatts.ToString()}, {data.GenerationRegister.AcPowerInWatts.ToString()} harvested ({data.GenerationRegister.EfficiencyInPercent}% efficiency, {data.GenerationRegister.EfficiencyPowerLossInWatts} watt loss)");

            if (stopwatch.ElapsedMilliseconds < Interval)
            {
                await Task.Delay((int) stopwatch.ElapsedMilliseconds - Interval, stoppingToken);
            }

            stopwatch.Reset();
        }
    }

    /// <summary>
    ///     Stops the worker when a cancellation request was sent (e.g. CTRL-C)
    /// </summary>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _service.Disconnect();

        await base.StopAsync(cancellationToken);
    }
}