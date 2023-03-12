using AutoMapper;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Serial.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Models;

namespace PowerBoss.Infra.Serial.Solar.Tests;

public class MapperTests
{
    private readonly IMapper _mapper;

    public MapperTests(IMapper mapper)
    {
        _mapper = mapper;
    }

    [Fact]
    public void ModelToDocumentTest()
    {
        InverterRegister inverterRegister = new()
        {
            Phase = Phase.ThreePhase
        };

        Register? model = _mapper.Map<InverterRegister, Register>(inverterRegister);
    }
}