using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Interfaces;

public interface ITeslaVehicleRepository
{
    public Task InsertOne(VehicleModel model, CancellationToken cancellationToken = default);
}