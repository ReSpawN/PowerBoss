using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public class Driver : ModelBase
{
    public string Name { get; }
    public string Email { get; }

    private Driver(
        Ulid ulid,
        string name,
        string email,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null
    ) : base(ulid, createdAt, updatedAt)
    {
        Name = name;
        Email = email;
    }

    public static Driver CreateNew(
        string? name,
        string? email
    ) =>
        new(
            ulid: Ulid.NewUlid(),
            name: Guard.Against.NullOrWhiteSpace(name, nameof(name)),
            email: Guard.Against.NullOrWhiteSpace(email, nameof(email)),
            createdAt: DateTimeOffset.UtcNow
        );
}