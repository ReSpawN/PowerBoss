using PowerBoss.Domain.Models;
using PowerBoss.Domain.Solar.Enums;

namespace PowerBoss.Domain.Solar.Models;

public sealed record Register : DtoBase
{
    public required Phase Phase { get; init; }
    public required RegisterAcCurrent AcCurrent { get; init; }
    public required RegisterAcVoltages AcVoltage { get; init; }
    public required RegisterGeneration Generation { get; init; }
    public required float ApparentPowerInVa { get; init; }
    public required float ReactivePowerInVar { get; init; }
    public required float HeatSinkTemperatureInCelsius { get; init; }
    public required OperatingState OperatingState { get; init; }
}