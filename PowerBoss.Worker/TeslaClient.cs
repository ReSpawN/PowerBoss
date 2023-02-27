using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Configuration;
using PowerBoss.Domain.Exceptions;
using PowerBoss.Domain.Models;
using PowerBoss.Domain.Models.Requests;
using PowerBoss.Domain.Models.Responses;

namespace PowerBoss.Worker;

public class TeslaClient
{
    private readonly HttpClient _client;
    private readonly IHttpClientFactory _factory;
    private readonly TeslaOptions _options;
    private Token _token;

    public TeslaClient(IOptions<TeslaOptions> options, IHttpClientFactory factory)
    {
        _factory = factory;
        _options = options.Value;

        Guard.Against.NullOrWhiteSpace(_options.FunctionalEndpoint);
        Guard.Against.NullOrWhiteSpace(_options.AccessToken);
        Guard.Against.NullOrWhiteSpace(_options.RefreshToken);

        _token = new Token(_options.AccessToken, _options.RefreshToken);

        _client = factory.CreateClient(GetType().Name);
        _client.BaseAddress = new Uri(_options.FunctionalEndpoint);
        _client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue(
                "Bearer",
                _token.AccessToken
            );

        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Tesla-Api", "1.0"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+http://cyntax.nl/powerboss)"));
    }

    public async Task<RefreshTokenResponse> RefreshToken(CancellationToken token = default)
    {
        Guard.Against.Null(_token);
        Guard.Against.NullOrWhiteSpace(_options.AuthenticationEndpoint);
        Guard.Against.NullOrWhiteSpace(_token.RefreshToken);

        HttpClient client = _factory.CreateClient(GetType().Name);
        client.BaseAddress = new Uri(_options.AuthenticationEndpoint);

        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("token", _token.ToRequest(), token);
        httpResponse.EnsureSuccessStatusCode();

        RefreshTokenResponse? response = await JsonSerializer.DeserializeAsync<RefreshTokenResponse>(
            await httpResponse.Content.ReadAsStreamAsync(token),
            cancellationToken: token
        );

        Guard.Against.Null(response);
        Guard.Against.NullOrWhiteSpace(response.AccessToken);
        Guard.Against.NullOrWhiteSpace(response.RefreshToken);

        _token = new Token(response.AccessToken, response.RefreshToken);

        return response;
    }

    public async Task<IEnumerable<Vehicle>> GetVehicles(CancellationToken token = default)
    {
        IEnumerable<Vehicle>? vehicles =
            await SendRequestUnwrapAsListResponse<IEnumerable<Vehicle>>(BuildGetRequest("vehicles"), token);

        return vehicles ?? Enumerable.Empty<Vehicle>();
    }

    private async Task<T?> SendRequest<T>(HttpRequestMessage req, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse = await _client.SendAsync(req, cancellationToken);

        if (httpResponse.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<T>(
                await httpResponse.Content.ReadAsStreamAsync(cancellationToken),
                cancellationToken: cancellationToken
            );
        }

        if (httpResponse.StatusCode == HttpStatusCode.RequestTimeout &&
            req.Options.TryGetValue(new HttpRequestOptionsKey<long>("vehicleId"), out long vehicleId))
        {
            // Attempt to wake the vehicle
            await CommandWake(vehicleId, cancellationToken);
            await Task.Delay(250, cancellationToken);
        }

        // @todo
        // string errorMessage = 
        throw new TeslaHttpException("Ging nie goed nie");
    }

    private async Task<T> SendRequestUnwrapAsListResponse<T>(HttpRequestMessage req, CancellationToken cancellationToken = default)
    {
        ListResponse<T>? response = await SendRequest<ListResponse<T>>(req, cancellationToken);

        Guard.Against.Null(response);
        Guard.Against.Null(response.Data);
        Guard.Against.NullOrInvalidInput(response.Count, nameof(response.Count), predicate: i => i > 0, "No values were returned.");

        return response.Data;
    }

    private async Task<T> SendRequestUnwrapAsDataResponse<T>(HttpRequestMessage req, CancellationToken cancellationToken = default)
    {
        Response<T>? response = await SendRequest<Response<T>>(req, cancellationToken);

        Guard.Against.Null(response);
        Guard.Against.Null(response.Data);

        return response.Data;
    }

    private async Task<CommandResponse> SendRequestUnwrapAsCommandResponse(HttpRequestMessage req, CancellationToken cancellationToken = default)
    {
        CommandResponse response = await SendRequestUnwrapAsDataResponse<CommandResponse>(req, cancellationToken);

        Guard.Against.Null(response);

        return response;
    }

    private static HttpRequestMessage BuildPostRequest(string uri, object? body = null)
        => BuildRequest(HttpMethod.Post, uri, body);

    private static HttpRequestMessage BuildPostRequest(string uri, Dictionary<string, object>? options, object? body = null)
        => BuildRequest(HttpMethod.Post, uri, body, options: options);

    private static HttpRequestMessage BuildPostRequest(string uri, long vehicleId, object? body = null)
    {
        Dictionary<string, object> options = new()
        {
            {
                "vehicleId", vehicleId
            }
        };

        return BuildRequest(HttpMethod.Post, uri, body, options: options);
    }

    private static HttpRequestMessage BuildGetRequest(string uri, IDictionary<string, string>? queryParams = null)
        => BuildRequest(HttpMethod.Get, queryParams is not null ? QueryHelpers.AddQueryString(uri, queryParams) : uri);

    private static HttpRequestMessage BuildRequest(
        HttpMethod method, string uri, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, object>? options = null)
    {
        HttpRequestMessage request = new(method, uri);

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

    public async Task<VehicleChargeState> GetVehicleChargingState(long? vehicleId, CancellationToken token = default)
    {
        VehicleChargeState state =
            await SendRequestUnwrapAsDataResponse<VehicleChargeState>(BuildGetRequest($"vehicles/{vehicleId}/data_request/charge_state"), token);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleChargeState> CommandWake(long? vehicleId, CancellationToken token = default)
    {
        Guard.Against.Null(vehicleId);

        VehicleChargeState state =
            await SendRequestUnwrapAsDataResponse<VehicleChargeState>(BuildPostRequest($"vehicles/{vehicleId}/wake_up", vehicleId), token);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleDriveState> GetVehicleDriveState(long? vehicleId, CancellationToken token = default)
    {
        VehicleDriveState state =
            await SendRequestUnwrapAsDataResponse<VehicleDriveState>(BuildGetRequest($"vehicles/{vehicleId}/data_request/drive_state"), token);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<VehicleGuiSettings> GetVehicleGuiSettings(long? vehicleId, CancellationToken token = default)
    {
        VehicleGuiSettings settings =
            await SendRequestUnwrapAsDataResponse<VehicleGuiSettings>(BuildGetRequest($"vehicles/{vehicleId}/data_request/gui_settings"), token);

        // return settings ?? throw new ArgumentNullException(nameof(settings));
        return settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<VehicleState> GetVehicleState(long? vehicleId, CancellationToken token = default)
    {
        VehicleState state =
            await SendRequestUnwrapAsDataResponse<VehicleState>(BuildGetRequest($"vehicles/{vehicleId}/data_request/vehicle_state"), token);

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    public async Task<CommandResponse> CommandLightFlash(long? vehicleId, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest($"vehicles/{vehicleId}/command/flash_lights"), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargePortOpen(long? vehicleId, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest($"vehicles/{vehicleId}/command/charge_port_door_open"), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargePortClose(long? vehicleId, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest($"vehicles/{vehicleId}/command/charge_port_door_close"), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargeStart(long? vehicleId, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest($"vehicles/{vehicleId}/command/charge_start"), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> CommandChargeStop(long? vehicleId, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(BuildPostRequest($"vehicles/{vehicleId}/command/charge_stop"), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetChargeLimit(long? vehicleId, int limit, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest($"vehicles/{vehicleId}/command/set_charge_limit", new SetChargeLimitRequest(limit)), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetChargingAmps(long? vehicleId, int amps, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest($"vehicles/{vehicleId}/command/set_charging_amps", new SetChargingAmpsRequest(amps)), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<CommandResponse> SetScheduledCharging(long? vehicleId, TimeOnly time, CancellationToken token = default)
    {
        CommandResponse response =
            await SendRequestUnwrapAsCommandResponse(
                BuildPostRequest($"vehicles/{vehicleId}/command/set_scheduled_charging", new SetScheduledChargingRequest(true, time)), token);

        return response ?? throw new ArgumentNullException(nameof(response));
    }
}