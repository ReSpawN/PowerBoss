namespace PowerBoss.Domain.Models;

public abstract class ModelBase
{
    public Ulid Ulid { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; protected set; }

    protected ModelBase(
        Ulid ulid,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null
    )
    {
        Ulid = ulid;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}