using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using EasyModbus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerBoss.Infra.Serial.Solar.Configuration;
using PowerBoss.Infra.Serial.Solar.Extensions;
using PowerBoss.Infra.Serial.Solar.Interfaces;
using PowerBoss.Infra.Serial.Solar.Models;

namespace PowerBoss.Infra.Serial.Solar;

public class Inverter : IInverter
{
    private const int SUNSPEC_COMMON_MODEL_REGISTER_ADDRESS_START = 40000;
    private const int SUNSPEC_INVERTER_REGISTER_ADDRESS_START = 40069;
    private const int SUNSPEC_METER_ONE_REGISTER_ADDRESS_START = 40122;
    private const int SUNSPEC_SOLAR_EDGE_DC_POWER = 40100;
    private readonly ILogger<Inverter> _logger;

    private readonly IOptions<InverterOptions> _options;
    private ModbusClient? _client;

    public Inverter(
        IOptions<InverterOptions> options,
        ILogger<Inverter> logger
    )
    {
        _options = options;
        _logger = logger;
    }

    public event ConnectionEvent? Connected;
    public event ConnectionEvent? Disconnected;

    public bool Connect()
    {
        if (_client is not null)
        {
            _logger.LogError("Already connected with this client.");
            throw new InvalidOperationException("Already connected");
        }

        _client = new ModbusClient(_options.Value.Address, _options.Value.Port)
        {
            ConnectionTimeout = _options.Value.ConnectTimeoutInMilliseconds
        };

        _client.ConnectedChanged += ConnectionChanged;

        try
        {
            _logger.LogInformation($"Connecting to inverter at {_client.IPAddress}:{_client.Port}");
            _client.Connect();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Disconnect()
    {
        _logger.LogInformation("Disconnecting");

        if (_client is not null)
        {
            _client.Disconnect();
            _client = null;
        }
    }

    [DoesNotReturn]
    private static void VerifyConnectionOrThrow([ValidatedNotNull] ModbusClient? client)
    {
        if (client is null)
        {
            throw new InvalidOperationException("No client is yet defined. Connect first.");
        }

        if (!client.Connected)
        {
            throw new InvalidOperationException("The client is not connected.");
        }
    }

    public Task<float> GetCurrentGeneration()
    {
        VerifyConnectionOrThrow(_client);

        int[] response = _client.ReadHoldingRegisters(SUNSPEC_SOLAR_EDGE_DC_POWER, 2);

        double power = response[0] * Math.Pow(10, response[1]);

        return Task.FromResult(Convert.ToSingle(power));
    }

    public Task<T> Get<T>()
        where T : class
    {
        VerifyConnectionOrThrow(_client);

        _logger.LogInformation($"Reading '{typeof(T).Name}' from inverter at {_client.IPAddress}:{_client.Port}");
        T result = _client.Read<T>();
        _logger.LogInformation($"Reading of '{typeof(T).Name}' successful");

        return Task.FromResult(result);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ConnectionChanged(object sender)
    {
        if (_client is null)
        {
            throw new InvalidOperationException("Client is not defined.");
        }

        if (_client.Connected)
        {
            Connected?.Invoke(this);
        }
        else
        {
            Connected?.Invoke(this);
        }
    }

    private void ReleaseUnmanagedResources()
    {
        Disconnect();
    }

    ~Inverter()
    {
        ReleaseUnmanagedResources();
    }
}