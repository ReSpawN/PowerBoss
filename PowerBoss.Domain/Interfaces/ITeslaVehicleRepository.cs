using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Interfaces;

public interface ITeslaVehicleRepository
{
    Task<Vehicle> InsertOne(Vehicle model, CancellationToken cancellationToken = default);
    Task<Vehicle> FindByUlid(Ulid ulid);
    Task<IEnumerable<Vehicle>> FindAll();
}