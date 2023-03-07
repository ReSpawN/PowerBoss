using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("vehicles")]
public sealed class VehicleDocument
{
    [BsonElement(Order = 0)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonElement(Order = 1)]
    public Ulid Ulid { get; set; }

    public string Name { get; set; }
    public long ExternalId { get; set; }
    public string State { get; set; }
    public string IdentificationNumber { get; set; }

    [BsonRepresentation(BsonType.String)]
    public required DateTimeOffset CreatedOn { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset? UpdatedOn { get; set; }
}