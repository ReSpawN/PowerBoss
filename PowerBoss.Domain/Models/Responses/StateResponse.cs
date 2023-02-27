using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Responses;

public class StateResponse<T>
{
    [JsonPropertyName("response")]
    public T? State { get; set; }
}