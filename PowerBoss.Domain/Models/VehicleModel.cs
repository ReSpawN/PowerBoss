namespace PowerBoss.Domain.Models;

public class VehicleModel
{
    public Guid? Guid { get; }
    public string? Name { get; set; }

    public VehicleModel()
    {
        Guid = new Guid();
    }
}