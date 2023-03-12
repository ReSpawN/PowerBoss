using AutoMapper;
using MongoDB.Driver;
using PowerBoss.Domain.Solar.Interfaces;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Documents.Inverter;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("solar")]
public sealed class SolarRegisterRepository : RepositoryBase<RegisterDocument, Register>, ISolarRegisterRepository
{
    public SolarRegisterRepository(
        IMongoClient client,
        IMapper mapper
    ) : base(client, mapper)
    {
    }
}