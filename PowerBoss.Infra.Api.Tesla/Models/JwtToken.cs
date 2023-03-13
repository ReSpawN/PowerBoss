using Ardalis.GuardClauses;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Api.Tesla.Models.Requests;

namespace PowerBoss.Infra.Api.Tesla.Models;

public class JwtToken
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init;  }

    public static JwtToken FromToken(Token token)
        => new()
        {
            AccessToken = Guard.Against.NullOrWhiteSpace(token.AccessToken),
            RefreshToken = Guard.Against.NullOrWhiteSpace(token.RefreshToken)
        };

    public RefreshTokenRequest ToRequest() => new(RefreshToken);
}