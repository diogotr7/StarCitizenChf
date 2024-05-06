using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable UnassignedGetOnlyAutoProperty
namespace ChfParser;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[JsonConverter(typeof(ColorConverter))]
public readonly struct Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
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
        throw new NotImplementedException();
    }
}