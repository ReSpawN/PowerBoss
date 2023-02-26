using System.Net.Http.Headers;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Configuration;
using PowerBoss.Domain.Models.Responses;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Worker;

public class TeslaClient
{
    private readonly HttpClient _client;

    public TeslaClient(IOptions<TeslaOptions> options, IHttpClientFactory factory)
    {
        Guard.Against.NullOrWhiteSpace(options.Value.Endpoint);
        Guard.Against.NullOrWhiteSpace(options.Value.AccessToken);
        Guard.Against.NullOrWhiteSpace(options.Value.RefreshToken);
        
        _client = factory.CreateClient(GetType().Name);
        _client.BaseAddress = new Uri(options.Value.Endpoint);
        _client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue(
                "Bearer",
                options.Value.AccessToken
            );
    }

    public async Task<IEnumerable<VehicleSynopsis>> GetVehicles(CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("vehicles", token);
        httpResponse.EnsureSuccessStatusCode();

        var vehicleList = await JsonSerializer.DeserializeAsync<ListVehiclesResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return vehicleList?.Vehicles ?? Enumerable.Empty<VehicleSynopsis>();
    }

    public async Task<VehicleChargeState?> GetVehicleChargingState(VehicleSynopsis vehicle, CancellationToken token = default) =>
        await GetVehicleChargingState(vehicle.Id, token);

    public async Task<VehicleChargeState?> GetVehicleChargingState(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/charge_state", token);
        httpResponse.EnsureSuccessStatusCode();

        var response = await JsonSerializer.DeserializeAsync<VehicleChargeStateResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.State;
    }

    public async Task<VehicleDriveState?> GetVehicleDriveState(VehicleSynopsis vehicle, CancellationToken token = default) =>
        await GetVehicleDriveState(vehicle.Id, token);

    public async Task<VehicleDriveState?> GetVehicleDriveState(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/drive_state", token);
        httpResponse.EnsureSuccessStatusCode();

        var response = await JsonSerializer.DeserializeAsync<VehicleDriveStateResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.State;
    }

    public async Task<VehicleGuiSettings?> GetVehicleGuiSettings(VehicleSynopsis vehicle, CancellationToken token = default) =>
        await GetVehicleGuiSettings(vehicle.Id, token);

    public async Task<VehicleGuiSettings?> GetVehicleGuiSettings(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/gui_settings", token);
        httpResponse.EnsureSuccessStatusCode();

        var response = await JsonSerializer.DeserializeAsync<VehicleGuiSettingsResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.Settings;
    }

    public async Task<VehicleState?> GetVehicleState(VehicleSynopsis vehicle, CancellationToken token = default) =>
        await GetVehicleState(vehicle.Id, token);

    public async Task<VehicleState?> GetVehicleState(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/vehicle_state", token);
        httpResponse.EnsureSuccessStatusCode();

        var response = await JsonSerializer.DeserializeAsync<VehicleStateResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.State;
    }
}