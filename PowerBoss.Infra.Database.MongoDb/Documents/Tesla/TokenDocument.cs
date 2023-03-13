using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("token")]
public sealed record TokenDocument : DocumentBase
{
    public required Ulid DriverUlid { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset? ExpiresAt { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset? RefreshedAt { get; set; }
}