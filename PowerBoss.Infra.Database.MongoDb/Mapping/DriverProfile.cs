using AutoMapper;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Mapping;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<DriverDocument, Driver>()
            .ReverseMap()
            .ForMember(destinationMember: d => d.Id, memberOptions: expression => expression.Ignore());
    }
}