using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("vehicles")]
public sealed class VehicleDocument
{
    [BsonElement(Order = 1)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    // [BsonRepresentation(BsonType.String)]
    [BsonElement(Order = 2)]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Ulid Uuid { get; set; }

    public string? Name { get; set; }
    
    [BsonDefaultValue("currentDate")]
    public DateTimeOffset CreatedOn { get; set; }
    
                                                                                                                                                                                                                                                                                                                                   public DateTimeOffset? UpdatedOn { get; set; }
}