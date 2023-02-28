using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Responses;

public class CommandResponse
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
    
    [JsonPropertyName("result")]
    public bool Result { get; set; }
}