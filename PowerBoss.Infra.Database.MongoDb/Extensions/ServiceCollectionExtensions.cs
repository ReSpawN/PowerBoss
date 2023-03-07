using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Repositories;
using PowerBoss.Infra.Database.MongoDb.Serializers;

namespace PowerBoss.Infra.Database.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(s =>
        {
            IOptions<MongoDbOptions> options = s.GetRequiredService<IOptions<MongoDbOptions>>();

            ConventionRegistry.Register("Filter null values", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreIfNullConvention(true),
                new EnumRepresentationConvention(BsonType.String)
            }, filter: _ => true);

            BsonSerializer.RegisterSerializer(UlidSerializer.Instance.ValueType, UlidSerializer.Instance);

            ILoggerFactory loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddSimpleConsole();
                b.SetMinimumLevel(LogLevel.Debug);
            });

            MongoClientSettings? settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
            settings.LoggingSettings = new LoggingSettings(loggerFactory);

            return new MongoClient(settings);
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ITeslaDriverRepository, TeslaDriverRepository>();
        services.AddSingleton<ITeslaTokenRepository, TeslaTokenRepository>();
        services.AddSingleton<ITeslaVehicleRepository, TeslaVehicleRepository>();
    }
}