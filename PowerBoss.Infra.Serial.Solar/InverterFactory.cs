using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Solar.Interfaces;
using PowerBoss.Infra.Serial.Solar.Configuration;
using PowerBoss.Infra.Serial.Solar.Interfaces;

namespace PowerBoss.Infra.Serial.Solar;

public class InverterFactory : IInverterFactory
{
    private readonly ILoggerFactory _factory;
    private readonly IMapper _mapper;
    private readonly IOptions<InverterOptions> _options;

    public InverterFactory(
        IOptions<InverterOptions> options,
        ILoggerFactory factory,
        IMapper mapper
    )
    {
        _options = options;
        _factory = factory;
        _mapper = mapper;
    }

    // @todo maybe do something along the lines of AddHttpClient
    public IInverterClient Create()
        => new InverterClient(_options, _factory.CreateLogger<InverterClient>(), _mapper);
}