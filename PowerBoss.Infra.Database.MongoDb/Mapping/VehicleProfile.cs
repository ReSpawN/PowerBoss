using AutoMapper;
using PowerBoss.Domain.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Mapping;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<VehicleDocument, Vehicle>()
            .ReverseMap()
            .ForMember(d => d.Id, expression => expression.Ignore());
    }
}