using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents;

public abstract class DocumentBase
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public Ulid Ulid { get; set; }

    [BsonRepresentation(BsonType.String)]
    public required DateTimeOffset CreatedAt { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset? UpdatedAt { get; set; }
}