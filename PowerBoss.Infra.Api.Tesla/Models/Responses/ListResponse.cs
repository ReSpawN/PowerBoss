using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Responses;

public class ListResponse<T>
{
    [JsonPropertyName("response")]
    public T? Data { get; set; }

    [JsonPropertyName("count")]
    public int? Count { get; set; }
}