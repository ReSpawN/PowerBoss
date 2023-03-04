using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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

    public async Task<VehicleModel> InsertOne(VehicleModel model, CancellationToken cancellationToken = default)
    {
        VehicleDocument? document = _mapper.Map<VehicleDocument>(model);
        
        // List<UpdateDefinition<VehicleDocument>> updateDefinitionList =
        //     document.GetType().GetProperties()
        //         .Select(x => Builders<VehicleDocument>.Update.Set(x.Name, x.GetValue(document))).ToList();
        //
        // await _collection.UpdateOneAsync(
        //     Builders<VehicleDocument>.Filter
        //         .Eq(d => d.Id, document.Id),
        //     Builders<VehicleDocument>.Update.Combine(updateDefinitionList)
        //         .SetOnInsert(d => d.CreatedOn, DateTimeOffset.UtcNow)
        //         .CurrentDate(d => d.UpdatedOn),
        //     new UpdateOptions
        //     {
        //         IsUpsert = true
        //     },
        //     cancellationToken
        // );

        await _collection.InsertOneAsync(
            document,
            new InsertOneOptions()
            {
                
            }
        );

        return _mapper.Map<VehicleModel>(document);
    }

    public async Task FindByUuid()
    {
    }
}