using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChfParser;

public class HexStringJsonConverter : JsonConverter<uint>
{
    public override uint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        return uint.Parse(s[2..], NumberStyles.HexNumber);
    }

    public override void Write(Utf8JsonWriter writer, uint value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"0x{value:X8}");
    }
}