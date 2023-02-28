using PowerBoss.Infra.Api.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models.Responses;

namespace PowerBoss.Worker.Extensions;

public static class TeslaClientExtensions
{
    public static async Task<VehicleChargeState> GetVehicleChargingState(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.GetVehicleChargingState(vehicle.Id, token);

    public static async Task<VehicleChargeState> CommandWake(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandWake(vehicle.Id, token);

    public static async Task<VehicleDriveState> GetVehicleDriveState(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.GetVehicleDriveState(vehicle.Id, token);

    public static async Task<VehicleGuiSettings> GetVehicleGuiSettings(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.GetVehicleGuiSettings(vehicle.Id, token);

    public static async Task<VehicleState> GetVehicleState(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.GetVehicleState(vehicle.Id, token);

    public static async Task<CommandResponse> CommandLightFlash(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandLightFlash(vehicle.Id, token);

    public static async Task<CommandResponse> CommandChargePortOpen(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandChargePortOpen(vehicle.Id, token);

    public static async Task<CommandResponse> CommandChargePortClose(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandChargePortClose(vehicle.Id, token);

    public static async Task<CommandResponse> CommandChargeStart(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandChargeStart(vehicle.Id, token);

    public static async Task<CommandResponse> CommandChargeStop(this TeslaClient client, Vehicle vehicle, CancellationToken token = default)
        => await client.CommandChargeStop(vehicle.Id, token);

    public static async Task<CommandResponse> SetChargingAmps(this TeslaClient client, Vehicle vehicle, int amps, CancellationToken token = default)
        => await client.SetChargingAmps(vehicle.Id, amps, token);

    public static async Task<CommandResponse> SetChargeLimit(this TeslaClient client, Vehicle vehicle, int limit, CancellationToken token = default)
        => await client.SetChargeLimit(vehicle.Id, limit, token);

    public static async Task<CommandResponse> SetScheduledCharging(
        this TeslaClient client, Vehicle vehicle, TimeOnly time, CancellationToken token = default)
        => await client.SetScheduledCharging(vehicle.Id, time, token);
}