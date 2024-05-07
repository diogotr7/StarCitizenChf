using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChfParser;

public class ColorConverter : JsonConverter<Color>
{
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (str == null)
            throw new JsonException("Expected string");
        
        if (str.Length != 7 || str[0] != '#')
            throw new JsonException("Invalid color format");
        
        var r = byte.Parse(str[1..3], NumberStyles.HexNumber);
        var g = byte.Parse(str[3..5], NumberStyles.HexNumber);
        var b = byte.Parse(str[5..7], NumberStyles.HexNumber);
        
        return new Color(r, g, b);
    }
}