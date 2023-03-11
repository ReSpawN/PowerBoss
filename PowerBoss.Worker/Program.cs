using PowerBoss.Infra.Api.Tesla;
using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Extensions;
using PowerBoss.Infra.Serial.Solar;
using PowerBoss.Infra.Serial.Solar.Configuration;
using PowerBoss.Infra.Serial.Solar.Interfaces;
using PowerBoss.Worker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // services.AddHostedService<Worker>();
        // services.AddHostedService<Scheduler>();
        services.AddHostedService<InverterService>();
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

        services.AddOptions<InverterOptions>()
            .Bind(context.Configuration.GetRequiredSection(InverterOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        #endregion

        #region Database

        services.AddMongoDb();
        services.AddRepositories();
        services.AddDocumentMappers();

        #endregion

        #region Solar

        // @todo factory pattern?
        services.AddSingleton<IInverter, Inverter>();

        #endregion
    })
    .Build();

await host.RunAsync();