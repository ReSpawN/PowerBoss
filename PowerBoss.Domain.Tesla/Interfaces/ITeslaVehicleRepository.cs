using PowerBoss.Domain.Tesla.Models;

namespace PowerBoss.Domain.Tesla.Interfaces;

public interface ITeslaVehicleRepository : IRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindByDriverUlid(Ulid ulid);
}