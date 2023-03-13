using PowerBoss.Domain.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Dtos;

/// <summary>
///     Model representation of the holding register range for the
///     <a href="https://www.solaredge.com/sites/default/files/sunspec-implementation-technical-note.pdf">SolarEdge Inverter</a>
/// </summary>
[RegisterMapping(typeof(InverterRegisterDto))]
public record InverterRegisterDto
{
    [RegisterMapping(40070, RegisterType.UInt16, Size = 1)]
    public Phase Phase { get; set; }

    [RegisterMapping(40072)]
    public AcCurrentRegisterDto AcCurrentRegister { get; set; } = null!;

    [RegisterMapping(40077)]
    public AcVoltagesRegisterDto AcVoltageRegister { get; set; } = null!;

    [RegisterMapping(40084)]
    public GenerationRegisterDto GenerationRegister { get; set; } = null!;

    [RegisterMapping(40088, RegisterType.ScaledInt16, RegisterUnit.Va, ScalingFactorAddress = 40089)]
    public float ApparentPowerInVa { get; set; }

    [RegisterMapping(40090, RegisterType.ScaledInt16, RegisterUnit.Var, ScalingFactorAddress = 40091)]
    public float ReactivePowerInVar { get; set; }

    [RegisterMapping(40104, RegisterType.ScaledInt16, RegisterUnit.Celsius, ScalingFactorAddress = 40107)]
    public float HeatSinkTemperatureInCelsius { get; set; }

    [RegisterMapping(40108, RegisterType.UInt16)]
    public OperatingState OperatingState { get; set; }
}