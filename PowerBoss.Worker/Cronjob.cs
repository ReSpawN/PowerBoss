namespace PowerBoss.Worker;

public class Cronjob : BackgroundService
{
    private readonly ILogger<Cronjob> _logger;

    public Cronjob(ILogger<Cronjob> logger)
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