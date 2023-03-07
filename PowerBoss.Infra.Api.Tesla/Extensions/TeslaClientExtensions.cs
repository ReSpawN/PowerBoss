using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models.Responses;
using Vehicle = PowerBoss.Infra.Api.Tesla.Models.Vehicle;

namespace PowerBoss.Infra.Api.Tesla.Extensions;

public static class TeslaClientExtensions
{
    public static async Task<VehicleChargeState> GetVehicleChargingState(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.GetVehicleChargingState(token, vehicle.Id, ct);

    public static async Task<VehicleChargeState> CommandWake(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandWake(token, vehicle.Id, ct);

    public static async Task<VehicleDriveState> GetVehicleDriveState(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.GetVehicleDriveState(token, vehicle.Id, ct);

    public static async Task<VehicleGuiSettings> GetVehicleGuiSettings(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.GetVehicleGuiSettings(token, vehicle.Id, ct);

    public static async Task<VehicleState> GetVehicleState(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.GetVehicleState(token, vehicle.Id, ct);

    public static async Task<CommandResponse> CommandLightFlash(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandLightFlash(token, vehicle.Id, ct);

    public static async Task<CommandResponse> CommandChargePortOpen(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandChargePortOpen(token, vehicle.Id, ct);

    public static async Task<CommandResponse> CommandChargePortClose(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandChargePortClose(token, vehicle.Id, ct);

    public static async Task<CommandResponse> CommandChargeStart(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandChargeStart(token, vehicle.Id, ct);

    public static async Task<CommandResponse> CommandChargeStop(this TeslaClient client, Token token, Vehicle vehicle, CancellationToken ct = default)
        => await client.CommandChargeStop(token, vehicle.Id, ct);

    public static async Task<CommandResponse> SetChargingAmps(this TeslaClient client, Token token, Vehicle vehicle, int amps, CancellationToken ct = default)
        => await client.SetChargingAmps(token, vehicle.Id, amps, ct);

    public static async Task<CommandResponse> SetChargeLimit(this TeslaClient client, Token token, Vehicle vehicle, int limit, CancellationToken ct = default)
        => await client.SetChargeLimit(token, vehicle.Id, limit, ct);

    public static async Task<CommandResponse> SetScheduledCharging(this TeslaClient client, Token token, Vehicle vehicle, TimeOnly time, CancellationToken ct = default)
        => await client.SetScheduledCharging(token, vehicle.Id, time, ct);
}