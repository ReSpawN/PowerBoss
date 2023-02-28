using PowerBoss.Infra.Api.Tesla.Models.Requests;

namespace PowerBoss.Infra.Api.Tesla.Models;

public class Token
{
    public Token(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; }
    public string RefreshToken { get; }
    public RefreshTokenRequest ToRequest() => new(RefreshToken);
}