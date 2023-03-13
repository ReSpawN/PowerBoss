using PowerBoss.Domain.Solar.Enums;

namespace PowerBoss.Domain.Solar.Models;

public class RegisterGeneration
{
    public float AcPowerInWatts { get; init; }

    public float AcFrequencyInHertz { get; init; }

    public float PowerFactorInPercent { get; init; }

    public uint AcLifetimeProductionInWatts { get; init; }

    public float DcCurrentInAmps { get; init; }

    public float DcVoltageInVolts { get; init; }

    public float DcPowerInWatts { get; init; }

    public float EfficiencyInPercent => (float) Math.Round(AcPowerInWatts / DcPowerInWatts * 100, 2);
    public float EfficiencyPowerLossInWatts => (float) Math.Round(DcPowerInWatts - AcPowerInWatts, 1);
}