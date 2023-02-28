using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Requests;

public class SetChargeLimitRequest
{
    [JsonPropertyName("percent")]
    public int Limit { get; }

    public SetChargeLimitRequest(int limit)
    {
        Limit = limit;
    }
}