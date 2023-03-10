using AutoMapper;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Mapping;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<VehicleDocument, Vehicle>()
            .ReverseMap()
            .ForMember(destinationMember: d => d.Id, memberOptions: expression => expression.Ignore());
    }
}