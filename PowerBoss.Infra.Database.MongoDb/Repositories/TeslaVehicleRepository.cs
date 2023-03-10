using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("tesla")]
public sealed class TeslaVehicleRepository : RepositoryBase<VehicleDocument, Vehicle>, ITeslaVehicleRepository
{
    public TeslaVehicleRepository(
        IMongoClient client,
        IMapper mapper
    ) : base(client, mapper)
    {
    }

    public async Task<IEnumerable<Vehicle>> FindByDriverUlid(Ulid ulid)
    {
        List<VehicleDocument>? documents = await Collection.AsQueryable()
            .Where(d => d.DriverUlid == ulid)
            .ToListAsync();

        return documents.Select(_mapper.Map<Vehicle>);
    }
}