using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Requests;

public class SetChargingAmpsRequest
{
    [JsonPropertyName("charging_amps")]
    public int Amps { get; }

    public SetChargingAmpsRequest(int amps)
    {
        Amps = amps;
    }
}