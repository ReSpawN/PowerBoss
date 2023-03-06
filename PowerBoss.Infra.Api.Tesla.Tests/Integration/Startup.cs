using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using PowerBoss.Infra.Database.MongoDb.Extensions;

namespace PowerBoss.Infra.Api.Tesla.Tests.Integration;

public class Startup
{
    private MongoDbRunner _runner;
    private MongoClient _client;

    public void ConfigureServices(IServiceCollection services)
    {
        _runner = MongoDbRunner.Start();
        _client = new MongoClient(_runner.ConnectionString);
        
        services.AddMappers();
    }
}