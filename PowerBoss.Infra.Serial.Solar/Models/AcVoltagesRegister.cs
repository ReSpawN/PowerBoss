using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

[RegisterMapping(typeof(AcVoltagesRegister))]
public class AcVoltagesRegister
{
    [RegisterMapping(40077, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseABInVolts { get; set; }

    [RegisterMapping(40078, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseBCInVolts { get; set; }

    [RegisterMapping(40079, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseCAInVolts { get; set; }

    [RegisterMapping(40080, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseANInVolts { get; set; }

    [RegisterMapping(40081, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseBNInVolts { get; set; }

    [RegisterMapping(40082, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40083)]
    public float PhaseCNInVolts { get; set; }
}