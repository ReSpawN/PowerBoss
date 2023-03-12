using PowerBoss.Domain.Solar.Events;

namespace PowerBoss.Domain.Solar.Interfaces;

public interface ISolarService
{
    void OnDisconnect(Action action);
    void Connect();

    void Disconnect();

    Task LogRegisterInterval();
}