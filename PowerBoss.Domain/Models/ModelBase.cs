using PowerBoss.Domain.Interfaces;

namespace PowerBoss.Domain.Models;

public abstract class ModelBase : IIdentifiableModel
{
    public Ulid Ulid { get; init; } = Ulid.NewUlid();
    public DateTimeOffset? CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } // @todo change to init?
}