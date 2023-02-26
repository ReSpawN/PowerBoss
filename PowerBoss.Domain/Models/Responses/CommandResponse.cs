using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Responses;

public class CommandResponse
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
    
    [JsonPropertyName("result")]
    public bool Result { get; set; }
}