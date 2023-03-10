using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

[RegisterMapping(40072, Size = 5)]
public class AcCurrent
{
    [RegisterMapping(40072, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float Total { get; set; }

    [RegisterMapping(40073, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseA { get; set; }

    [RegisterMapping(40074, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseB { get; set; }

    [RegisterMapping(40075, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseC { get; set; }
}