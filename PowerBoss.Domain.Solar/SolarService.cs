using PowerBoss.Domain.Solar.Interfaces;
using PowerBoss.Domain.Solar.Models;

namespace PowerBoss.Domain.Solar;

public class SolarService : ISolarService
{
    private readonly IInverterClient _client;
    private readonly ISolarRegisterRepository _repository;

    public SolarService(
        IInverterClient client,
        ISolarRegisterRepository repository
    )
    {
        _client = client;
        _repository = repository;
    }

    public void OnDisconnect(Action action)
        => _client.Disconnected += _ => action.Invoke();

    public void Connect()
    {
        _client.Connect();
    }

    public void Disconnect()
    {
        _client.Disconnect();
    }

    public async Task LogRegisterInterval()
    {
        Register register = await _client.ReadRegister();

        await _repository.InsertOne(register);
    }
}