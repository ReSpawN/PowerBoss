using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

public class TeslaVehicleRepository : RepositoryBase<Vehicle>, ITeslaVehicleRepository
{
    private const string Database = "Tesla";

    public TeslaVehicleRepository(IOptions<MongoDbOptions> dbOptions) : base(dbOptions, Database)
    {
    }

    public async Task FindByUuid()
    {
    }
}