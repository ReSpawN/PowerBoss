using PowerBoss.Domain.Tesla.Models;

namespace PowerBoss.Domain.Tesla.Interfaces;

public interface ITeslaVehicleRepository
{
    Task<Vehicle> InsertOne(Vehicle model, CancellationToken cancellationToken = default);
    Task<Vehicle> FindByUlid(Ulid ulid);
    Task<IEnumerable<Vehicle>> FindAll();
}