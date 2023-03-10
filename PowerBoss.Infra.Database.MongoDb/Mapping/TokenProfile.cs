using AutoMapper;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;
// ReSharper disable UnusedType.Global

namespace PowerBoss.Infra.Database.MongoDb.Mapping;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<TokenDocument, Token>()
            .ReverseMap()
            .ForMember(destinationMember: d => d.Id, memberOptions: expression => expression.Ignore());
    }
}