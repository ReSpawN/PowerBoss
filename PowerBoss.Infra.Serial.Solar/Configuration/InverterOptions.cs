using System.ComponentModel.DataAnnotations;

namespace PowerBoss.Infra.Serial.Solar.Configuration;

public class InverterOptions
{
    public const string Section = "Inverter";
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "The TCP IP address for the SolarEdge Inverter.")]
    public required string Address { get; set; }

    [Range(502, 65536, ErrorMessage = "The port for the SolarEdge Inverter, usually either 502 or 1502.")]
    public int Port { get; set; } = 1502;

    public int ConnectTimeoutInMilliseconds { get; set; } = 1000;
}