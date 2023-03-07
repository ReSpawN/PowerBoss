using Microsoft.Extensions.DependencyInjection;
using PowerBoss.Infra.Database.MongoDb.Extensions;

namespace PowerBoss.Infra.Api.Tesla.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDocumentMappers();
        services.AddRepositories();
    }
}