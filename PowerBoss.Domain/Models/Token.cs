using PowerBoss.Domain.Models.Requests;
using PowerBoss.Domain.Models.Responses;

namespace PowerBoss.Domain.Models;

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