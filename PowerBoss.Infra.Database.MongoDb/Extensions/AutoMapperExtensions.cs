using Microsoft.Extensions.DependencyInjection;

namespace PowerBoss.Infra.Database.MongoDb.Extensions;

public static class AutoMapperExtensions
{
    public static void AddDocumentMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperExtensions).Assembly);
    }
}