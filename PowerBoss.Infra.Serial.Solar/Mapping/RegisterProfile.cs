using AutoMapper;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Serial.Solar.Dtos;

// ReSharper disable UnusedType.Global

namespace PowerBoss.Infra.Serial.Solar.Mapping;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<InverterRegisterDto, Register>()
            .ForMember(d => d.Generation, opt => opt.MapFrom(s => s.GenerationRegister))
            .ForMember(d => d.AcCurrent, opt => opt.MapFrom(s => s.AcCurrentRegister))
            .ForMember(d => d.AcVoltage, opt => opt.MapFrom(s => s.AcVoltageRegister));
        
        CreateMap<GenerationRegisterDto, RegisterGeneration>();
        CreateMap<AcVoltagesRegisterDto, RegisterAcVoltages>();
        CreateMap<AcCurrentRegisterDto, RegisterAcCurrent>();
    }

}