namespace PowerBoss.Domain.Solar.Models;

public record RegisterAcVoltages
{
    public required float PhaseABInVolts { get; init; }
    public required float PhaseBCInVolts { get; init; }
    public required float PhaseCAInVolts { get; init; }
    public required float PhaseANInVolts { get; init; }
    public required float PhaseBNInVolts { get; init; }
    public required float PhaseCNInVolts { get; init; }
}