using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public sealed class Vehicle : ModelBase
{
    public required Ulid DriverUlid { get; init; }
    public required string Name { get; init; }
    public required long ExternalId { get; init; }
    public required string IdentificationNumber { get; init; }
    public required string State { get; init; }
}