using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Configuration;
using PowerBoss.Infra.Api.Tesla.Exceptions;
using PowerBoss.Infra.Api.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models.Requests;
using PowerBoss.Infra.Api.Tesla.Models.Responses;
using Vehicle = PowerBoss.Infra.Api.Tesla.Models.Vehicle;

namespace PowerBoss.Infra.Api.Tesla;

public class TeslaClient
{
    private readonly HttpClient _client;
    private readonly IHttpClientFactory _factory;
    private readonly TeslaOptions _options;

    public TeslaClient(IOptions<TeslaOptions> options, IHttpClientFactory factory)
    {
        _factory = factory;
        _options = options.Value;

        _client = factory.CreateClient(GetType().Name);
        _client.BaseAddress = new Uri(Guard.Against.NullOrWhiteSpace(_options.FunctionalEndpoint));

        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Tesla-Api", "1.0"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+http://cyntax.nl/powerboss)"));
    }

    public async Task<RefreshTokenResponse> RefreshToken(Token token, CancellationToken ct = default)
    {
        Guard.Against.NullOrWhiteSpace(_options.AuthenticationEndpoint);
        Guard.Against.NullOrWhiteSpace(token.RefreshToken);

        HttpClient client = _factory.CreateClient(GetType().Name);
        client.BaseAddress = new Uri(_options.AuthenticationEndpoint);

        HttpResponseMessage httpResponse = await client.PostAsJsonAsync<RefreshTokenRequest>("token", JwtToken.FromToken(token).ToRequest(), ct);
        httpResponse.EnsureSuccessStatusCode();

        RefreshTokenResponse? response = await JsonSerializer.DeserializeAsync<RefreshTokenResponse>(
            await httpResponse.Content.ReadAsStreamAsync(ct),
            cancellationToken: ct
        );

        Guard.Against.Null(response);
        Guard.Against.NullOrWhiteSpace(response.AccessToken);
        Guard.Against.NullOrWhiteSpace(response.RefreshToken);

        return response;
    }

    public async Task<IEnumerable<Vehicle>> GetVehicles(Token token, CancellationToken ct = default)
    {
        IEnumerable<Vehicle>? vehicles =
            await SendRequestUnwrapAsListResponse<IEnumerable<Vehicle>>(BuildGetRequest(token, "vehicles"), ct);

        return vehicles ?? Enumerable.Empty<Vehicle>();
    }

    private async Task<T?> SendRequest<T>(HttpRequestMessage req, CancellationToken ct = default)
    {
        HttpResponseMessage httpResponse = await _client.SendAsync(req, ct);

        httpResponse.EnsureSuccessStatusCode();

        // if (httpResponse.IsSuccessStatusCode)
        // {
        return await JsonSerializer.DeserializeAsync<T>(
            await httpResponse.Content.ReadAsStreamAsync(ct),
            cancellationToken: ct
        );
        // }

        // These need to be wrapped in Polly
        // switch (httpResponse.StatusCode)
        // {
        //     case HttpStatusCode.Unauthorized:
        //         await RefreshToken(cancellationToken);
        //         break;
        //     case HttpStatusCode.RequestTimeout when req.Options.TryGetValue(new HttpRequestOptionsKey<long>("vehicleId"), out long vehicleId):
        //         // Attempt to wake the vehicle
        //         await CommandWake(token, vehicleId, cancellationToken);
        //         await Task.Delay(250, cancellationToken);
        //         break;
        // }

        // @todo
        // string errorMessage = 
        throw new TeslaHttpException("Ging nie goed nie");
    }

    private async Task<T> SendRequestUnwrapAsListResponse<T>(HttpRequestMessage req, CancellationToken ct = default)
    {
        ListResponse<T>? response = await SendRequest<ListResponse<T>>(req, ct);

        Guard.Against.Null(response);
        Guard.Against.Null(response.Data);
        Guard.Against.NullOrInvalidInput(response.Count, nameof(response.Count), predicate: i => i > 0, "No values were returned.");

        return response.Data;
    }

    private async Task<T> SendRequestUnwrapAsDataResponse<T>(HttpRequestMessage req, CancellationToken ct = default)
    {
        Response<T>? response = await SendRequest<Response<T>>(req, ct);

        Guard.Against.Null(response);
        Guard.Against.Null(response.Data);

        return response.Data;
    }

    private async Task<CommandResponse> SendRequestUnwrapAsCommandResponse(HttpRequestMessage req, CancellationToken ct = default)
    {
        CommandResponse response = await SendRequestUnwrapAsDataResponse<CommandResponse>(req, ct);

        Guard.Against.Null(response);

        return response;
    }

    private static HttpRequestMessage BuildPostRequest(Token token, string uri, object? body = null)
        => BuildRequest(HttpMethod.Post, token, uri, body);

    private static HttpRequestMessage BuildPostRequest(Token token, string uri, Dictionary<string, object>? options, object? body = null)
        => BuildRequest(HttpMethod.Post, token, uri, body, options: options);

    private static HttpRequestMessage BuildPostRequest(Token token, string uri, long vehicleId, object? body = null)
    {
        Dictionary<string, object> options = new()
        {
            {
                "vehicleId", vehicleId
            }
        };

        return BuildRequest(HttpMethod.Post, token, uri, body, options: options);
    }

    private static HttpRequestMessage BuildGetRequest(Token token, string uri, IDictionary<string, string>? queryParams = null)
        => BuildRequest(HttpMethod.Get, token, queryParams is not null ? QueryHelpers.AddQueryString(uri, queryParams) : uri);

    private static HttpRequestMessage BuildRequest(
        HttpMethod method, Token token, string uri, object? body = null, Dictionary<string, string>? headers = null,
        Dictionary<string, object>? options = null)
    {
        HttpRequestMessage request = new(method, uri)
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken)
            }
        };

        if (headers is not null && headers.Any())
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        if (body is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        }

        if (options is not null)
        {
            foreach (KeyValuePair<string, object> option in options)
            {
                request.Options.Set(new HttpRequestOptionsKey<object>(option.Key), option.Value);
            }
        }

        return request;
    }

    public async Task<VehicleChargeState> GetVehicleChargingState(Token token, long? vehicleId, CancellationToken ct = default)
    {
        VehicleChargeState state =
            await SendRequestUnwrapAsDataResponse<VehicleChargeState>(BuildGetRequest(token, $"vehicles/{vehicleId}/data_request/charge_state"), ct);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleChargeState> CommandWake(Token token, long? vehicleId, CancellationToken ct = default)
    {
        Guard.Against.Null(vehicleId);

        VehicleChargeState state =
            await SendRequestUnwrapAsDataResponse<VehicleChargeState>(BuildPostRequest(token, $"vehicles/{vehicleId}/wake_up", vehicleId), ct);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleDriveState> GetVehicleDriveState(Token token, long? vehicleId, CancellationToken ct = default)
    {
        VehicleDriveState state =
            await SendRequestUnwrapAsDataResponse<VehicleDriveState>(BuildGetRequest(token, $"vehicles/{vehicleId}/data_request/drive_state"), ct);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleGuiSettings> GetVehicleGuiSettings(Token token, long? vehicleId, CancellationToken ct = default)
    {
        VehicleGuiSettings settings =
            await SendRequestUnwrapAsDataResponse<VehicleGuiSettings>(BuildGetRequest(token, $"vehicles/{vehicleId}/data_request/gui_settings"), ct);

        // return settings ?? throw new ArgumentNullException(nameof(settings));
        return settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<VehicleState> GetVehicleState(Token token, long? vehicleId, CancellationToken ct = default)
    {
        VehicleState state =
            await SendRequestUnwrapAsDataResponse<VehicleState>(BuildGetRequest(token, $"vehicles/{vehicleId}/data_request/vehicle_state"), ct);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<CommandResponse> CommandLightFlash(Token token, long? vehicleId, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest(token, $"vehicles/{vehicleId}/command/flash_lights"), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargePortOpen(Token token, long? vehicleId, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest(token, $"vehicles/{vehicleId}/command/charge_port_door_open"), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargePortClose(Token token, long? vehicleId, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest(token, $"vehicles/{vehicleId}/command/charge_port_door_close"), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargeStart(Token token, long? vehicleId, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest(token, $"vehicles/{vehicleId}/command/charge_start"), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargeStop(Token token, long? vehicleId, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest(token, $"vehicles/{vehicleId}/command/charge_stop"), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetChargeLimit(Token token, long? vehicleId, int limit, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest(token, $"vehicles/{vehicleId}/command/set_charge_limit", new SetChargeLimitRequest(limit)), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetChargingAmps(Token token, long? vehicleId, int amps, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest(token, $"vehicles/{vehicleId}/command/set_charging_amps", new SetChargingAmpsRequest(amps)), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetScheduledCharging(Token token, long? vehicleId, TimeOnly time, CancellationToken ct = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest(token, $"vehicles/{vehicleId}/command/set_scheduled_charging", new SetScheduledChargingRequest(true, time)), ct);

        return response ?? throw new ArgumentNullException(nameof(response));
    }
}