using PowerBoss.Domain.Solar.Interfaces;

namespace PowerBoss.Infra.Serial.Solar.Interfaces;

public interface IInverterFactory
{
    IInverterClient Create();
}