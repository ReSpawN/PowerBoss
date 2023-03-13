using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public sealed class Token : ModelBase
{
    public required Ulid DriverUlid { get; set; }
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }
    public DateTimeOffset? RefreshedAt { get; private set; }

    public Token SetAccessToken(string? accessToken)
    {
        Guard.Against.NullOrWhiteSpace(accessToken);

        if (accessToken != AccessToken)
        {
            RefreshedAt = DateTimeOffset.UtcNow;
        }

        AccessToken = accessToken;

        return this;
    }

    public Token SetRefreshToken(string? refreshToken)
    {
        RefreshToken = Guard.Against.NullOrWhiteSpace(refreshToken);

        return this;
    }

    public Token SetExpireAt(DateTimeOffset? expiresAt)
    {
        ExpiresAt = Guard.Against.Null(expiresAt);

        return this;
    }

    public Token SetExpireAfter(TimeSpan? expiresIn)
    {
        ExpiresAt = DateTimeOffset.UtcNow.Add(Guard.Against.Null(expiresIn)!.Value);

        return this;
    }
}