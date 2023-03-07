using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public sealed class Vehicle : ModelBase
{
    public Ulid DriverUlid { get; }
    public string Name { get; }
    public long ExternalId { get; }
    public string IdentificationNumber { get; }
    public string State { get; }

    private Vehicle(
        Ulid ulid,
        Ulid driverUlid,
        string name,
        long externalId,
        string identificationNumber,
        string state,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null
    ) : base(ulid, createdAt, updatedAt)
    {
        DriverUlid = driverUlid;
        Name = name;
        ExternalId = externalId;
        IdentificationNumber = identificationNumber;
        State = state;
    }

    public static Vehicle CreateNew(
        Ulid driverUlid,
        string? name,
        long externalId,
        string? identificationNumber,
        string? state
    ) =>
        new(
            ulid: Ulid.NewUlid(),
            driverUlid: Guard.Against.Null(driverUlid, nameof(driverUlid)),
            name: Guard.Against.NullOrWhiteSpace(name, nameof(name)),
            externalId: Guard.Against.Null(externalId, nameof(externalId)),
            identificationNumber: Guard.Against.NullOrWhiteSpace(identificationNumber, nameof(identificationNumber)),
            state: Guard.Against.NullOrWhiteSpace(state, nameof(state)),
            createdAt: DateTimeOffset.UtcNow
        );
}