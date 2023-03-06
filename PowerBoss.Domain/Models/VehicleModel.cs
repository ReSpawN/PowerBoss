using Ardalis.GuardClauses;

namespace PowerBoss.Domain.Models;

public class VehicleModel : BaseModel
{
    public string Name { get; private set; }
    public DateTimeOffset? CreatedOn { get; private set; }
    public DateTimeOffset? UpdatedOn { get; private set; }

    private VehicleModel(
        Ulid uuid,
        string name,
        DateTimeOffset? createdOn = null,
        DateTimeOffset? updatedOn = null
    ) : base(uuid)
    {
        Name = name;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    public static VehicleModel CreateNew(string name)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));

        return new VehicleModel(
            uuid: Ulid.NewUlid(),
            name: name,
            createdOn: DateTimeOffset.UtcNow
        );
    }
}

public abstract class BaseModel
{
    public Ulid Uuid { get; private set; }

    protected BaseModel(Ulid uuid)
    {
        Uuid = uuid;
    }
}