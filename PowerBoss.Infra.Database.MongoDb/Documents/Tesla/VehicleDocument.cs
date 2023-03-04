using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("vehicles")]
public sealed class VehicleDocument
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonRepresentation(BsonType.String)]
    public Ulid Guid { get; set; }

    public string? Name { get; set; }
    
    public DateTimeOffset CreatedOn { get; set; }
    
                                                                                                                                                                                                                                                                                                                                   public DateTimeOffset? UpdatedOn { get; set; }
}