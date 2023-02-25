using System.Text.Json.Serialization;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Domain.Models.Responses;

public class VehicleChargeStateResponse
{
    [JsonPropertyName("response")]
    public VehicleChargeState? State { get; set; }
    
}