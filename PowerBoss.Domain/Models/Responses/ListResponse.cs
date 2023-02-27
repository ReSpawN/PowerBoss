using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Responses;

public class ListResponse<T>
{
    [JsonPropertyName("response")]
    public T? Data { get; set; }

    [JsonPropertyName("count")]
    public int? Count { get; set; }
}