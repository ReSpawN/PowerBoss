using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models.Requests;

namespace PowerBoss.Infra.Api.Tesla.Models;

public class JwtToken
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    private JwtToken(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static JwtToken FromToken(Token token)
        => new(token.AccessToken, token.RefreshToken);

    public RefreshTokenRequest ToRequest() => new(RefreshToken);
}