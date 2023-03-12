using AutoMapper;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Inverter;
// ReSharper disable UnusedType.Global

namespace PowerBoss.Infra.Database.MongoDb.Mapping;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<RegisterDocument, Register>()
            .ReverseMap()
            .ForMember(destinationMember: d => d.Id, memberOptions: expression => expression.Ignore());
    }
}