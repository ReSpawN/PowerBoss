using System.Reflection;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Linq;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Resolvers;
using PowerBoss.Infra.Database.MongoDb.Serializers;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

/// <summary>
///     A repository base implementation supporting the <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/logging/">MongoDb client</a>.
///
/// Naming conventions are fully supported. All naming strategies are the same, following the camelCase convention for Database, Collection and Field names.
/// Note that each database is suffixed with "Db" (e.g. "TeslaDb")
/// See <a href="https://www.mongodb.com/docs/manual/core/databases-and-collections/#databases">Database names</a> 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RepositoryBase<T>
    where T : class
{
    protected readonly IMongoCollection<T> Collection;
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    protected RepositoryBase(IMongoClient client)
    {
        _client = client;
        _database = GetDatabase();
        Collection = GetCollection();
    }

    private IMongoDatabase GetDatabase()
    {
        string databaseName = DatabaseAttributeResolver.Resolve(GetType());

        return _client.GetDatabase(databaseName+"Db");
    }

    private IMongoCollection<T> GetCollection()
    {
        string collectionName = CollectionAttributeResolver.Resolve<T>();

        List<string>? names = _database.ListCollectionNames().ToList();

        if (names is not null && names.Contains(collectionName))
        {
            return _database.GetCollection<T>(collectionName);
        }

        _database.CreateCollection(collectionName);

        return _database.GetCollection<T>(collectionName);
    }

    /// <summary>
    /// Query the repository's collection through a <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/linq/#std-label-csharp-linq">LINQ</a> interface.
    /// https://www.peerislands.io/timeseries/
    /// </summary>
    /// <returns></returns>
    protected IMongoQueryable<T> AsQueryable(AggregateOptions? options = null)
    {
        options ??= new AggregateOptions()
        {
            Collation = new Collation("en", strength: CollationStrength.Primary)
        };

        return Collection.AsQueryable(options);
    }
}