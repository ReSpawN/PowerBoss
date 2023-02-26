using System.Text.Json.Serialization;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Domain.Models.Responses;

public class VehicleGuiSettingsResponse
{
    [JsonPropertyName("response")]
    public VehicleGuiSettings? Settings { get; set; }
}