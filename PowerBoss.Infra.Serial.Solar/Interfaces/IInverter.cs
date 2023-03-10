using PowerBoss.Infra.Serial.Solar.Models;

namespace PowerBoss.Infra.Serial.Solar.Interfaces;

public interface IInverter
{
    event ConnectionEvent Connected;
    event ConnectionEvent Disconnected;

    bool Connect();
    void Disconnect();

    Task<float> GetCurrentGeneration();

    Task<T> Get<T>() where T : class;
}

public delegate void ConnectionEvent(object sender);