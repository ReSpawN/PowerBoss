using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Requests;

public class SetScheduledChargingRequest
{
    [JsonPropertyName("enable")]
    public bool Enabled { get; }

    [JsonPropertyName("time")]
    public int Time { get; }

    public SetScheduledChargingRequest(bool enabled, TimeOnly time)
    {
        Enabled = enabled;
        Time = (time.Hour * 60) + time.Second;
    }
}