using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

/// <summary>
///     Model representation of the holding register range for the
///     <a href="https://www.solaredge.com/sites/default/files/sunspec-implementation-technical-note.pdf">SolarEdge Inverter</a>
/// </summary>
[RegisterMapping(40070, Size = 40)]
public class InverterRegister
{
    [RegisterMapping(40070, RegisterType.UInt16)]
    public Phase Phase { get; set; }

    [RegisterMapping(40072)]
    public AcCurrent AcCurrent { get; set; }

    [RegisterMapping(40077)]
    public AcVoltages AcComparison { get; set; }

    [RegisterMapping(40084, RegisterType.ScaledInt16, RegisterUnit.Watts, ScalingFactorAddress = 40085)]
    public float AcPowerValue { get; set; }

    [RegisterMapping(40086, RegisterType.ScaledUInt16, RegisterUnit.Hertz, ScalingFactorAddress = 40087)]
    public float AcFrequency { get; set; }

    [RegisterMapping(40088, RegisterType.ScaledInt16, RegisterUnit.VA, ScalingFactorAddress = 40089)]
    public float ApparentPower { get; set; }

    [RegisterMapping(40090, RegisterType.ScaledInt16, RegisterUnit.VAR, ScalingFactorAddress = 40091)]
    public float ReactivePower { get; set; }

    [RegisterMapping(40092, RegisterType.ScaledInt16, RegisterUnit.Percent, ScalingFactorAddress = 40093)]
    public float PowerFactor { get; set; }

    [RegisterMapping(40094, RegisterType.ScaledAcc32, RegisterUnit.WattHours, ScalingFactorAddress = 40096)]
    public uint AcLifetimeProduction { get; set; }

    [RegisterMapping(40097, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40098)]
    public float DcCurrentValue { get; set; }

    [RegisterMapping(40099, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40100)]
    public float DcVoltageValue { get; set; }

    [RegisterMapping(40101, RegisterType.ScaledInt16, RegisterUnit.Watts, ScalingFactorAddress = 40102)]
    public float DcPowerValue { get; set; }

    [RegisterMapping(40104, RegisterType.ScaledInt16, RegisterUnit.Celcius, ScalingFactorAddress = 40107)]
    public float HeatSinkTemperature { get; set; }

    [RegisterMapping(40108, RegisterType.UInt16)]
    public OperatingState OperatingState { get; set; }
}