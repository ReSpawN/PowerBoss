using Microsoft.Extensions.DependencyInjection;

namespace PowerBoss.Infra.Serial.Solar.Extensions;

public static class AutoMapperExtensions
{
    public static void AddRegisterMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperExtensions).Assembly);
    }
}