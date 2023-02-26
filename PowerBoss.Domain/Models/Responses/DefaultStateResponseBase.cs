using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models.Responses;

public abstract class DefaultStateResponseBase<TResponse>
{
    [JsonPropertyName("response")]
    public TResponse? State { get; set; }
}