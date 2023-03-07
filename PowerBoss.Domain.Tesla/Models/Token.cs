using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public class Token : ModelBase
{
    public Ulid DriverUlid { get; set; }
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }
    public DateTimeOffset? RefreshedAt { get; private set; }

    private Token(
        Ulid ulid,
        Ulid driverUlid,
        string accessToken,
        string refreshToken,
        DateTimeOffset? refreshedAt = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null
    ) : base(ulid, createdAt, updatedAt)
    {
        DriverUlid = driverUlid;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        RefreshedAt = refreshedAt;
    }

    public static Token CreateNew(
        Ulid driverUlid,
        string? accessToken,
        string? refreshToken
    ) =>
        new(
            ulid: Ulid.NewUlid(),
            driverUlid: Guard.Against.Null(driverUlid),
            accessToken: Guard.Against.NullOrWhiteSpace(accessToken, nameof(accessToken)),
            refreshToken: Guard.Against.NullOrWhiteSpace(refreshToken, nameof(refreshToken)),
            createdAt: DateTimeOffset.UtcNow
        );

    public Token SetAccessToken(string? accessToken)
    {
        Guard.Against.NullOrWhiteSpace(accessToken);

        if (accessToken != AccessToken)
        {
            RefreshedAt = DateTimeOffset.UtcNow;
        }
        
        AccessToken = accessToken;
        UpdatedAt = DateTimeOffset.UtcNow;

        return this;
    }

    public Token SetRefreshToken(string? refreshToken)
    {
        RefreshToken = Guard.Against.NullOrWhiteSpace(refreshToken);
        UpdatedAt = DateTimeOffset.UtcNow;

        return this;
    }

    public Token SetExpireAt(DateTimeOffset? expiresAt)
    {
        ExpiresAt = Guard.Against.Null(expiresAt);
        UpdatedAt = DateTimeOffset.UtcNow;

        return this;
    }

    public Token SetExpireAfter(TimeSpan? expiresIn)
    {
        ExpiresAt = DateTimeOffset.UtcNow.Add(Guard.Against.Null(expiresIn)!.Value);
        UpdatedAt = DateTimeOffset.UtcNow;

        return this;
    }
}