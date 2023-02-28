using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<Cronjob>();
        services.AddHttpClient();

        services.AddSingleton<TeslaClient>();

        services.AddOptions<TeslaOptions>()
            .Bind(context.Configuration.GetRequiredSection(TeslaOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    })
    .Build();

await host.RunAsync();