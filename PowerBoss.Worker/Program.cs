using PowerBoss.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient();

        services.AddSingleton<TeslaClient>();
    })
    .Build();

await host.RunAsync();