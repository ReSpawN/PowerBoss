namespace PowerBoss.Domain.Models;

public abstract class ModelBase
{
    public Ulid Ulid { get; }
    public DateTimeOffset? CreatedOn { get; }
    public DateTimeOffset? UpdatedOn { get; }

    protected ModelBase(
        Ulid ulid,
        DateTimeOffset? createdOn = null,
        DateTimeOffset? updatedOn = null
    )
    {
        Ulid = ulid;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }
}