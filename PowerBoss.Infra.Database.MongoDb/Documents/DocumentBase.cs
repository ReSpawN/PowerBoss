using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents;

public abstract record DocumentBase
{
    public ObjectId Id { get; init; }

    public Ulid Ulid { get; init; }

    [BsonRepresentation(BsonType.String)]
    public required DateTimeOffset CreatedAt { get; init; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset? UpdatedAt { get; set; }
}