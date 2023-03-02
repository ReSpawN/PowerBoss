using Microsoft.Extensions.DependencyInjection;
using PowerBoss.Domain.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Extensions;

public static class AutoMapperExtensions
{
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(VehicleDocument), typeof(VehicleModel));
    }
    
}