using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Infra.Database.MongoDb;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Extensions;
using PowerBoss.Infra.Database.MongoDb.Serializers;
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

        services.AddMongoDb();

        #endregion

        #region Database

        services.AddMappers();
        services.AddRepositories();
        // services.AddMongoServices();

        #endregion
    })
    .Build();

await host.RunAsync();