using System.Net.Http.Headers;
using System.Text.Json;
using PowerBoss.Domain.Models;
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
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Im5ZdVZJWTJTN3gxVHRYM01KMC1QMDJad3pXQSJ9.eyJpc3MiOiJodHRwczovL2F1dGgudGVzbGEuY29tL29hdXRoMi92MyIsImF1ZCI6WyJodHRwczovL293bmVyLWFwaS50ZXNsYW1vdG9ycy5jb20vIiwiaHR0cHM6Ly9hdXRoLnRlc2xhLmNvbS9vYXV0aDIvdjMvdXNlcmluZm8iXSwiYXpwIjoib3duZXJhcGkiLCJzdWIiOiJhYzQ1OTcxMC02ZThjLTRiYWEtOWFkMC0zZmIyODg2MDg3MTgiLCJzY3AiOlsib3BlbmlkIiwiZW1haWwiLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicHdkIiwibWZhIiwib3RwIl0sImV4cCI6MTY3NzM1MjYwMiwiaWF0IjoxNjc3MzIzODAyLCJhdXRoX3RpbWUiOjE2NzczMjM4MDF9.d8yOnbPWZJpgK_HRZeLY8b3mqB5PzCdKNAzIl-iiMKaRlmF0iTOMkca8e47cOdHbBDLIQHXhp6jXnL221CDmtVYDP072Ld9w0lUUc42GpAcuWIyjyewUi8UfewaX02pOU_h02x6SLqHN-Y2_5CNsluSD75iP6Tby0OARz0Gd3t8RBZbepmeZXSWTIOQO0LyhFGyGVdw353u_KZ_dgJ2wuTfyVv6miuBvZ17urkCziDfi1UsnfYeDS4iOghFnVvjW3_Prgfy5snQh5Hb0TC4JC0AiFUSOx3kWFLs9ZJdOhTAwQCIzy8X7T85XLxBqE2GSNKnwpy630QU8TDV4iFFm-g"
            );
    }

    public async Task<IEnumerable<VehicleSynopsis>> GetVehicles(CancellationToken token = default)
    {
        HttpResponseMessage response = await _client.GetAsync("vehicles", token);
        response.EnsureSuccessStatusCode();

        var vehicleList = await JsonSerializer.DeserializeAsync<ListVehiclesResponse>(
            await response.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return vehicleList?.Vehicles ?? Enumerable.Empty<VehicleSynopsis>();
    }

    public async Task<VehicleChargeState?> GetVehicleChargingState(VehicleSynopsis vehicle, CancellationToken token = default)
    {
        return await GetVehicleChargingState(vehicle.Id, token);
    }

    public async Task<VehicleChargeState?> GetVehicleChargingState(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/charge_state", token);

        var response = await JsonSerializer.DeserializeAsync<VehicleChargeStateResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.State;
    }

    public async Task<VehicleDriveState?> GetVehicleDriveState(VehicleSynopsis vehicle, CancellationToken token = default)
    {
        return await GetVehicleDriveState(vehicle.Id, token);
    }

    public async Task<VehicleDriveState?> GetVehicleDriveState(long? vehicleId, CancellationToken token = default)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"vehicles/{vehicleId}/data_request/drive_state", token);

        var response = await JsonSerializer.DeserializeAsync<VehicleDriveStateResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        return response?.State;
    }
}