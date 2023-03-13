using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using AutoMapper;
using EasyModbus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerBoss.Domain.Solar.Events;
using PowerBoss.Domain.Solar.Interfaces;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Serial.Solar.Configuration;
using PowerBoss.Infra.Serial.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Extensions;
using PowerBoss.Infra.Serial.Solar.Dtos;
#pragma warning disable CS8763

namespace PowerBoss.Infra.Serial.Solar;

public class InverterClient : IInverterClient
{
    private readonly IOptions<InverterOptions> _options;
    private readonly ILogger<InverterClient> _logger;
    private readonly IMapper _mapper;
    private ModbusClient? _client;

    public InverterClient(
        IOptions<InverterOptions> options,
        ILogger<InverterClient> logger,
        IMapper mapper
    )
    {
        _options = options;
        _logger = logger;
        _mapper = mapper;
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
            _logger.LogInformation("Connected");

            return true;
        }
        catch (Exception)
        {
            _logger.LogWarning("Failed to connect");
            return false;
        }
    }

    public void Disconnect()
    {
        if (_client is not null)
        {
            _logger.LogInformation("Disconnecting");

            _client.Disconnect();
            _client = null;
            
            _logger.LogInformation("Disconnected");
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Async wrapper for reading to prevent thread locking.
    /// </summary>
    public Register ReadRegister()
    {
        InverterRegisterDto dto = Read<InverterRegisterDto>();

        return _mapper.Map<Register>(dto);
    }

    private T Read<T>()
        where T : class
    {
        VerifyClientConnectionOrThrow(_client);

        _logger.LogDebug($"Reading '{typeof(T).Name}' from inverter at {_client.IPAddress}:{_client.Port}");
        T result = _client.Read<T>();
        _logger.LogDebug($"Reading of '{typeof(T).Name}' successful");

        return result;
    }

    [DoesNotReturn]
    private static void VerifyClientConnectionOrThrow([ValidatedNotNull] ModbusClient? client)
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
            Disconnected?.Invoke(this);
        }
    }

    private void ReleaseUnmanagedResources()
    {
        _logger.LogInformation("Disconnecting through ReleaseUnmanagedResources");
        Disconnect();
    }

    ~InverterClient()
    {
        ReleaseUnmanagedResources();
    }
}