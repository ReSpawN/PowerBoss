using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

[RegisterMapping(typeof(AcCurrentRegister))]
public class AcCurrentRegister
{
    [RegisterMapping(40072, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float TotalInAmps { get; set; }

    [RegisterMapping(40073, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseAInAmps { get; set; }

    [RegisterMapping(40074, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseBInAmps { get; set; }

    [RegisterMapping(40075, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40076)]
    public float PhaseCInAmps { get; set; }
}