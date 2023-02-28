using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Responses;

public class StateResponse<T>
{
    [JsonPropertyName("response")]
    public T? State { get; set; }
}