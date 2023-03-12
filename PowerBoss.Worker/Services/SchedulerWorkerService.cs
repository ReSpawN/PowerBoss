namespace PowerBoss.Worker.Services;

public class SchedulerWorkerService : BackgroundService
{
    private readonly ILogger<SchedulerWorkerService> _logger;

    public SchedulerWorkerService(ILogger<SchedulerWorkerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogDebug("Cronjob running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}