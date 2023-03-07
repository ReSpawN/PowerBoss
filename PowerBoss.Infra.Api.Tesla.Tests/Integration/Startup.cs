using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Extensions;
using PowerBoss.Infra.Database.MongoDb.Repositories;
using Xunit.DependencyInjection;

namespace PowerBoss.Infra.Api.Tesla.Tests.Integration;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        MongoDbRunner runner = MongoDbRunner.Start();

        services.AddOptions<MongoDbOptions>()
            .Configure(options => options.ConnectionString = runner.ConnectionString);

        services.AddMongoDb();
        services.AddMappers();
        
        services.AddSingleton<ITeslaVehicleRepository, TeslaVehicleRepository>();
    }
}