using AutoMapper;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Serial.Solar.Models;

// ReSharper disable UnusedType.Global

namespace PowerBoss.Infra.Serial.Solar.Mapping;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<InverterRegister, Register>();
    }
}