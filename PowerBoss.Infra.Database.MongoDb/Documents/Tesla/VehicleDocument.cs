using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("vehicles")]
public sealed record VehicleDocument : DocumentBase
{
    public required Ulid DriverUlid { get; set; }
    public required string Name { get; set; }
    public required long ExternalId { get; set; }
    public required string State { get; set; }
    public required string IdentificationNumber { get; set; }
}