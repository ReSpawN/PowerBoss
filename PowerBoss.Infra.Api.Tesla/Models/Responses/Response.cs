using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Responses;

public class Response<T>
{
    [JsonPropertyName("response")]
    public T Data { get; set; }
}