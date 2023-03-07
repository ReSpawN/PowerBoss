using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

[Database("tesla")]
public sealed class TeslaTokenRepository : RepositoryBase<TokenDocument, Token>, ITeslaTokenRepository
{
    public TeslaTokenRepository(
        IMongoClient client,
        IMapper mapper
    ) : base(client, mapper)
    {
    }

    public async Task<Token> FindByDriverUlid(Ulid ulid)
    {
        TokenDocument document = await Collection.AsQueryable()
            .Where(d => d.DriverUlid == ulid)
            .FirstAsync();

        return _mapper.Map<Token>(document);
    }
}