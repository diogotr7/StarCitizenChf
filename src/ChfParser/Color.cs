using System.Runtime.InteropServices;
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