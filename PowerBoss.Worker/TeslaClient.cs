using System.Net.Http.Headers;
using System.Text.Json;
using PowerBoss.Domain.Models.Responses;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Worker;

public class TeslaClient
{
    private readonly HttpClient _client;

    public TeslaClient(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(GetType().Name);
        _client.BaseAddress = new Uri("https://owner-api.teslamotors.com/api/1/");
        _client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue(
                "Bearer",
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Im5ZdVZJWTJTN3gxVHRYM01KMC1QMDJad3pXQSJ9.eyJpc3MiOiJodHRwczovL2F1dGgudGVzbGEuY29tL29hdXRoMi92MyIsImF1ZCI6WyJodHRwczovL293bmVyLWFwaS50ZXNsYW1vdG9ycy5jb20vIiwiaHR0cHM6Ly9hdXRoLnRlc2xhLmNvbS9vYXV0aDIvdjMvdXNlcmluZm8iXSwiYXpwIjoib3duZXJhcGkiLCJzdWIiOiJhYzQ1OTcxMC02ZThjLTRiYWEtOWFkMC0zZmIyODg2MDg3MTgiLCJzY3AiOlsib3BlbmlkIiwiZW1haWwiLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicHdkIiwibWZhIiwib3RwIl0sImV4cCI6MTY3NzQzNzgxNSwiaWF0IjoxNjc3NDA5MDE1LCJhdXRoX3RpbWUiOjE2Nzc0MDkwMTR9.PdlrC79Dl3xQ9MIgDXshH4vDPIOCr0VTI6OUSXnoe7bG35PZuquWxxW7ivmPgKhCXIX6xcgR9NNKNm6225KrdgcT_TFEjD9TeUcW5vB6feL5OIMqGWQLA_UEhkwVFpvpX7K-UeMisFLVkaO0-BTD8vBz2m70Gw6LF1UlygHb1zRmourneUUtDYBLkAAMLEGXRff9T8f93VV0YRC-3jrru5120SLjWkxe92wgcCZ3H6wqR6beD3pJP7UlaqFBw-_Tn9r72Lm4FI0dH06jODTrxDq30OUXPKufxKCun3dDa-ZhTB9HSf3zJqKXIUxXxgahyDI6OMv0E1660evJdfqrjw"
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