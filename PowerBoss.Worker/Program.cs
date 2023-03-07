using PowerBoss.Infra.Api.Tesla;
using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Extensions;
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

        services.AddMongoDb();
        services.AddRepositories();
        services.AddDocumentMappers();

        #endregion
    })
    .Build();

await host.RunAsync();