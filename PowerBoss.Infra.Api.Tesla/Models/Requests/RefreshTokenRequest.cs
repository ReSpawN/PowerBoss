using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models.Requests;

public class RefreshTokenRequest
{
    public RefreshTokenRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "refresh_token";

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = "ownerapi";

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; } = "openid email offline_access";
}