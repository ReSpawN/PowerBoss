using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerBoss.Infra.Serial.Solar.Configuration;
using PowerBoss.Infra.Serial.Solar.Interfaces;

namespace PowerBoss.Infra.Serial.Solar;

public class InverterFactory : IInverterFactory
{
    private readonly IOptions<InverterOptions> _options;
    private readonly ILoggerFactory _factory;

    public InverterFactory(
        IOptions<InverterOptions> options,
        ILoggerFactory factory
        )
    {
        _options = options;
        _factory = factory;
    }

    // @todo maybe do something along the lines of AddHttpClient
    public IInverter Create() 
        => new Inverter(_options, _factory.CreateLogger<Inverter>());
}