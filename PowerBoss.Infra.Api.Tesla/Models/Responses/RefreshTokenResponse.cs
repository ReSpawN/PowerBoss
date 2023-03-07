using System.Text.Json.Serialization;
using PowerBoss.Infra.Api.Tesla.Converters;

namespace PowerBoss.Infra.Api.Tesla.Models.Responses;

public class RefreshTokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("id_token")]
    public string? IdToken { get; set; }

    [JsonPropertyName("expires_in")]
    [JsonConverter(typeof(IntToTimeSpanJsonConverter))]
    public TimeSpan? ExpiresAfter { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}