using System.Reflection;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PowerBoss.Domain.Interfaces;
using PowerBoss.Domain.Models;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Infra.Database.MongoDb.Documents;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;
using PowerBoss.Infra.Database.MongoDb.Resolvers;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

/// <summary>
///     A repository base implementation supporting the
///     <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/logging/">MongoDb client</a>.
///     Naming conventions are fully supported. All naming strategies are the same, following the camelCase convention for Database, Collection and Field
///     names.
///     Note that each database is suffixed with "Db" (e.g. "TeslaDb")
///     See <a href="https://www.mongodb.com/docs/manual/core/databases-and-collections/#databases">Database names</a>
/// </summary>
/// <typeparam name="TDocument"></typeparam>
/// <typeparam name="TModel"></typeparam>
public abstract class RepositoryBase<TDocument, TModel> : IRepository<TModel>
    where TDocument : DocumentBase
    where TModel : ModelBase
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    protected readonly IMapper _mapper;
    protected readonly IMongoCollection<TDocument> Collection;

    protected RepositoryBase(IMongoClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;

        _database = GetDatabase();
        Collection = GetCollection();
    }

    private IMongoDatabase GetDatabase()
    {
        string databaseName = DatabaseAttributeResolver.Resolve(GetType());

        return _client.GetDatabase(databaseName + "Db");
    }

    private IMongoCollection<TDocument> GetCollection()
    {
        string collectionName = CollectionAttributeResolver.Resolve<TDocument>();

        List<string>? names = _database.ListCollectionNames().ToList();

        if (names is not null && names.Contains(collectionName))
        {
            return _database.GetCollection<TDocument>(collectionName);
        }

        _database.CreateCollection(collectionName);

        return _database.GetCollection<TDocument>(collectionName);
    }

    /// <summary>
    ///     Query the repository's collection through a
    ///     <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/linq/#std-label-csharp-linq">LINQ</a> interface.
    ///     https://www.peerislands.io/timeseries/
    /// </summary>
    /// <returns></returns>
    protected IMongoQueryable<TDocument> AsQueryable(AggregateOptions? options = null)
    {
        options ??= new AggregateOptions
        {
            Collation = new Collation("en", strength: CollationStrength.Primary)
        };

        return Collection.AsQueryable(options);
    }

    public async Task<IEnumerable<TModel>> FindAll()
    {
        List<TDocument>? result = await AsQueryable()
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();

        return result.Select(_mapper.Map<TModel>);
    }

    public async Task<TModel> InsertOne(TModel model, CancellationToken ct = default)
    {
        TDocument? document = _mapper.Map<TDocument>(model);

        await Collection.InsertOneAsync(document, cancellationToken: ct);

        return _mapper.Map<TModel>(document);
    }

    public async Task<TModel> FindByUlid(Ulid ulid)
    {
        TDocument document = await Collection.AsQueryable()
            .Where(d => d.Ulid == ulid)
            .FirstAsync();

        return _mapper.Map<TModel>(document);
    }

    public async Task<TModel> UpdateByUlid(Ulid tokenUlid, TModel model)
    {
        TDocument? document = _mapper.Map<TDocument>(model);
        
        UpdateDefinitionBuilder<TDocument> builder = Builders<TDocument>.Update;
        List<UpdateDefinition<TDocument>> updates = new()
        {
            // builder.Set(d => d.UpdatedAt, DateTimeOffset.UtcNow) // @todo responsibility lies in the domain? 
        };

        PropertyInfo[] properties = document.GetType()
            .GetProperties(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance);
        
        foreach (PropertyInfo property in properties)
        {
            if (typeof(ObjectId) == property.PropertyType)
            {
                continue;
            }
            
            updates.Add(builder.Set(property.Name, property.GetValue(document)));
        }

        await Collection.UpdateOneAsync(
            Builders<TDocument>.Filter.Eq(d => d.Ulid, document.Ulid),
            builder.Combine(updates)
        );

        return _mapper.Map<TModel>(document);
    }
}