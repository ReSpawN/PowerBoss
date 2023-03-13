namespace PowerBoss.Domain.Solar.Models;

public record RegisterAcCurrent
{
    public required float TotalInAmps { get; init; }
    public required float PhaseAInAmps { get; init; }
    public required float PhaseBInAmps { get; init; }
    public required float PhaseCInAmps { get; init; }
}