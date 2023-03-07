using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("tesla")]
public class TeslaVehicleRepository : RepositoryBase<VehicleDocument>, ITeslaVehicleRepository
{
    private readonly IMapper _mapper;

    public TeslaVehicleRepository(IMongoClient client, IMapper mapper)
        : base(client)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<Vehicle>> FindAll()
    {
        List<VehicleDocument>? result = await AsQueryable()
            .OrderByDescending(f => f.CreatedOn)
            .ToListAsync();

        return result.Select(d => _mapper.Map<Vehicle>(d));
    }

    public async Task<Vehicle> InsertOne(Vehicle model, CancellationToken cancellationToken = default)
    {
        VehicleDocument? document = _mapper.Map<VehicleDocument>(model);

        await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);

        return _mapper.Map<Vehicle>(document);
    }

    public async Task<Vehicle> FindByUlid(Ulid ulid)
    {
        VehicleDocument document = await Collection.AsQueryable()
            .Where(d => d.Ulid == ulid)
            .FirstAsync();
        
        return _mapper.Map<Vehicle>(document);
    }
}