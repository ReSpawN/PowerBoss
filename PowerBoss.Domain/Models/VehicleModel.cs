namespace PowerBoss.Domain.Models;

public class VehicleModel
{
    public required Ulid Guid { get; set; }
    public string? Name { get; set; }
    
    public required DateTimeOffset? CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }


    public static VehicleModel CreateNew()
    {
        return new VehicleModel
        {
            Guid = Ulid.NewUlid(),
            CreatedOn = DateTimeOffset.UtcNow
        };
    }

}