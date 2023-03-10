using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

[RegisterMapping(40077, Size = 7)]
public class AcVoltages 
{
    [RegisterMapping(40077, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseAB { get; set; }

    [RegisterMapping(40078, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseBC { get; set; }

    [RegisterMapping(40079, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseCA { get; set; }

    [RegisterMapping(40080, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseAN { get; set; }

    [RegisterMapping(40081, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseBN { get; set; }

    [RegisterMapping(40082, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseCN { get; set; }
}