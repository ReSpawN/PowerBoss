using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Converters;

public class IntToBooleanJsonConverter : JsonConverter<bool>
{
    public override bool CanConvert(Type objectType)
        => objectType == typeof(bool);

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Convert.ToBoolean(reader.GetInt32());

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        => writer.WriteNumberValue(Convert.ToInt32(value));
}