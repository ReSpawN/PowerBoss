using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Converters;

public class TimestampToDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
    private static readonly long UnixMinSeconds = DateTime.MinValue.Ticks / TimeSpan.TicksPerSecond - DateTimeOffset.UnixEpoch.Ticks / TimeSpan.TicksPerSecond;
    private static readonly long UnixMaxSeconds = DateTime.MaxValue.Ticks / TimeSpan.TicksPerSecond - DateTimeOffset.UnixEpoch.Ticks / TimeSpan.TicksPerSecond;

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        long x = reader.GetInt64();

        if (x < UnixMinSeconds)
        {
            x *= 1000;
        }

        if (x >= UnixMaxSeconds)
        {
            x /= 1000;
        }

        return DateTimeOffset.FromUnixTimeSeconds(x);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToUnixTimeSeconds());
}