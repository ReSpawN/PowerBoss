using PowerBoss.Domain.Solar.Events;
using PowerBoss.Domain.Solar.Models;

namespace PowerBoss.Domain.Solar.Interfaces;

public interface IInverterClient : IDisposable
{
    event ConnectionEvent Connected;
    event ConnectionEvent Disconnected;
    bool Connect();
    void Disconnect();
    Register ReadRegister();
}