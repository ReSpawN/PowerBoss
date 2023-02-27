using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Responses;

public class Response<T>
{
    [JsonPropertyName("response")]
    public T Data { get; set; }
}