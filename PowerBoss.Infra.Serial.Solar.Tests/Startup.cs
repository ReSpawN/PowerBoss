using Microsoft.Extensions.DependencyInjection;
using PowerBoss.Infra.Database.MongoDb.Extensions;
using PowerBoss.Infra.Serial.Solar.Extensions;

namespace PowerBoss.Infra.Serial.Solar.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRegisterMappers();
        services.AddDocumentMappers();
        
    }
}