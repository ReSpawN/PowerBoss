using System.Text.Json.Serialization;
using PowerBoss.Domain.Converters;

namespace PowerBoss.Domain.Models.Responses;

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
    public TimeSpan? ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}