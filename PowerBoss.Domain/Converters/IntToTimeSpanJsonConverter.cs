using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Converters;

public class IntToTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override bool CanConvert(Type objectType)
        => objectType == typeof(TimeSpan);

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TimeSpan.FromSeconds(reader.GetInt32());

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.Seconds);
}