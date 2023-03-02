using AutoMapper;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Domain.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("tesla")]
public class TeslaVehicleRepository : RepositoryBase<VehicleDocument>, ITeslaVehicleRepository
{
    private readonly IMapper _mapper;

    public TeslaVehicleRepository(IOptions<MongoDbOptions> dbOptions, IMapper mapper) : base(dbOptions)
    {
        _mapper = mapper;
    }

    public async Task InsertOne(VehicleModel model, CancellationToken cancellationToken = default)
    {
        VehicleDocument? document = _mapper.Map<VehicleDocument>(model);
        
        await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);
    }

    public async Task FindByUuid()
    {
    }
}