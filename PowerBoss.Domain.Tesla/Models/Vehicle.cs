using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public sealed class Vehicle : ModelBase
{
    public string Name { get; }
    public long ExternalId { get; }
    public string IdentificationNumber { get; }
    public string State { get; }

    private Vehicle(
        Ulid ulid,
        string name,
        long externalId,
        string identificationNumber,
        string state,
        DateTimeOffset? createdOn = null,
        DateTimeOffset? updatedOn = null
    ) : base(ulid, createdOn, updatedOn)
    {
        Name = name;
        ExternalId = externalId;
        IdentificationNumber = identificationNumber;
        State = state;
    }

    public static Vehicle CreateNew(
        string? name,
        long externalId,
        string? identificationNumber,
        string? state
    ) =>
        new(
            ulid: Ulid.NewUlid(),
            name: Guard.Against.NullOrWhiteSpace(name, nameof(name)),
            externalId: Guard.Against.Null(externalId, nameof(externalId)),
            identificationNumber: Guard.Against.NullOrWhiteSpace(identificationNumber, nameof(identificationNumber)),
            state: Guard.Against.NullOrWhiteSpace(state, nameof(state)),
            createdOn: DateTimeOffset.UtcNow
        );
}