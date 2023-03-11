namespace PowerBoss.Worker.Services;

public class Scheduler : BackgroundService
{
    private readonly ILogger<Scheduler> _logger;

    public Scheduler(ILogger<Scheduler> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Cronjob running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}