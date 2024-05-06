using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable UnassignedGetOnlyAutoProperty
namespace ChfParser;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[JsonConverter(typeof(ColorConverter))]
public readonly struct Color(byte r, byte g, byte b)
{
    public byte R { get; } = r;
    public byte G { get; } = g;
    public byte B { get; } = b;

    //Alpha seems to be unused. Keep it for alignment.
    private readonly byte _A;
    
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}

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