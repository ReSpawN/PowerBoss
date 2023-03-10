using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Models;

/// <summary>
///     Model representation of the holding register range for the
///     <a href="https://www.solaredge.com/sites/default/files/sunspec-implementation-technical-note.pdf">SolarEdge Inverter</a>
/// </summary>
[RegisterMapping(typeof(Generation))]
public class Generation
{
    [RegisterMapping(40094, RegisterType.ScaledAcc32, RegisterUnit.WattHours, ScalingFactorAddress = 40096)]
    public uint AcLifetimeProductionInWatts { get; set; }

    [RegisterMapping(40097, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40098)]
    public float DcCurrentInAmps { get; set; }
    
    [RegisterMapping(40099, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40100)]
    public float DcVoltageInVolts { get; set; }
    
    [RegisterMapping(40101, RegisterType.ScaledInt16, RegisterUnit.Watts, ScalingFactorAddress = 40102)]
    public float DcPowerInWatts { get; set; }
    
    [RegisterMapping(40104, RegisterType.ScaledInt16, RegisterUnit.Celcius, ScalingFactorAddress = 40107)]
    public float HeatSinkTemperatureInCelsius { get; set; }
    
    [RegisterMapping(40108, RegisterType.UInt16)]
    public OperatingState OperatingState { get; set; }
}