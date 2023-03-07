using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PowerBoss.Infra.Api.Tesla.Models.Requests;

public class SetScheduledChargingRequest
{
    [JsonPropertyName("enable")]
    public bool Enabled { get; }

    [JsonPropertyName("time")]
    public int MinutesSinceLocalMidnight { get; }

    protected static readonly int[] quarters = {
        0,
        15,
        30,
        45
    };

    public SetScheduledChargingRequest(bool enabled, TimeOnly time)
    {
        Enabled = enabled;

        if (time.Second > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time), "Cannot define seconds on this property.");
        }

        if (!quarters.Contains(time.Minute))
        {
            throw new ArgumentOutOfRangeException(nameof(time), $"The Minute property can only be a factor of 15 (e.g. {string.Join(", ", quarters)})");
        }

        MinutesSinceLocalMidnight = time.Hour * 60 + time.Minute;
    }
}