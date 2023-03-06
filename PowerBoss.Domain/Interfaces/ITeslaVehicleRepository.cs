using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Interfaces;

public interface ITeslaVehicleRepository
{
    Task<VehicleModel> InsertOne(VehicleModel model, CancellationToken cancellationToken = default);
    Task<IEnumerable<VehicleModel>> FindAll();
}