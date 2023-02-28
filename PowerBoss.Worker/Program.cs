using PowerBoss.Domain.Interfaces;
using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Repositories;
using PowerBoss.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<Cronjob>();
        services.AddHttpClient();

        services.AddSingleton<TeslaClient>();

        #region Configuration

        services.AddOptions<TeslaOptions>()
            .Bind(context.Configuration.GetRequiredSection(TeslaOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<MongoDbOptions>()
            .Bind(context.Configuration.GetRequiredSection(MongoDbOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        #endregion

        #region Database

        services.AddSingleton<ITeslaVehicleRepository, TeslaVehicleRepository>();

        #endregion
    })
    .Build();

await host.RunAsync();