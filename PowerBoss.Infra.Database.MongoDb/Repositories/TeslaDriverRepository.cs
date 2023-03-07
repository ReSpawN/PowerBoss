using AutoMapper;
using MongoDB.Driver;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("tesla")]
public sealed class TeslaDriverRepository : RepositoryBase<DriverDocument, Driver>, ITeslaDriverRepository
{
    public TeslaDriverRepository(
        IMongoClient client,
        IMapper mapper
    ) : base(client, mapper)
    {
    }
}