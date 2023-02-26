using System.Text.Json.Serialization;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Domain.Models.Responses;

public class ListVehiclesResponse
{
    [JsonPropertyName("response")]
    public IEnumerable<VehicleSynopsis>? Vehicles { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}