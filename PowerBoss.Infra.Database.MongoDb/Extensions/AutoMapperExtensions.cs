using Microsoft.Extensions.DependencyInjection;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Domain.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;
using PowerBoss.Infra.Database.MongoDb.Repositories;

namespace PowerBoss.Infra.Database.MongoDb.Extensions;

public static class AutoMapperExtensions
{
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperExtensions).Assembly);
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ITeslaVehicleRepository, TeslaVehicleRepository>();
    }
}