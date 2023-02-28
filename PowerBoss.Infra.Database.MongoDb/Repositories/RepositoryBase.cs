﻿using System.Reflection;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Configuration;
using PowerBoss.Infra.Database.MongoDb.Resolvers;

namespace PowerBoss.Infra.Database.MongoDb.Repositories;

/// <summary>
///     A repository base implementation supporting the <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/logging/">MongoDb client</a>.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RepositoryBase<T>
    where T : class
{
    protected readonly IMongoCollection<T> _collection;
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    protected RepositoryBase(IOptions<MongoDbOptions> dbOptions, string databaseName)
    {
        ConventionRegistry.Register("Filter null values", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreIfNullConvention(true),
            new EnumRepresentationConvention(BsonType.String),
        }, _ => true);
        
        using ILoggerFactory loggerFactory = LoggerFactory.Create(b =>
        {
            b.AddSimpleConsole();
            b.SetMinimumLevel(LogLevel.Debug);
        });

        MongoClientSettings? settings = MongoClientSettings.FromConnectionString(dbOptions.Value.ToConnectionString());
        settings.LoggingSettings = new LoggingSettings(loggerFactory);

        _client = new MongoClient(settings);
        _database = _client.GetDatabase(databaseName);
        _collection = _database.GetCollection<T>(CollectionAttributeResolver.Resolve<T>());
    }

    /// <summary>
    /// Query the repository's collection through a <a href="https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/linq/#std-label-csharp-linq">LINQ</a> interface.
    /// </summary>
    /// <returns></returns>
    protected IQueryable<T> AsQueryable(AggregateOptions? options = null)
    {
        options ??= new AggregateOptions()
        {
            Collation = new Collation("en", strength: CollationStrength.Primary)
        };
        
        return _collection.AsQueryable(options);
    }
}