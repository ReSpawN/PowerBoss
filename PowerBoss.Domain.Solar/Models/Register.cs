using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Solar.Models;

public sealed class Register : ModelBase
{
    public required ushort Phase { get; init; }

    public Register(
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null
    )
        : base(Ulid.NewUlid(), createdAt ?? DateTimeOffset.UtcNow, updatedAt)
    {
    }

    // public Register(
    //     Ulid ulid,
    //     ushort phase,
    //     DateTimeOffset? createdAt = null,
    //     DateTimeOffset? updatedAt = null
    // )
    //     : base(ulid, createdAt, updatedAt)
    // {
    //     Phase = phase;
    // }
    //
    // public static Register CreateNew(ushort phase) =>
    //     new(
    //         Ulid.NewUlid(),
    //         phase,
    //         DateTimeOffset.UtcNow
    //     );
}