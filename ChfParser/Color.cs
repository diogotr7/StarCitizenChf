using System.Runtime.InteropServices;

namespace ChfParser;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct Color
{
    public readonly byte B;
    public readonly byte G;
    public readonly byte R;
    //Alpha seems to be unused. Keep it for alignment.
    private readonly byte _A;
    
    public string Hex => $"#{R:X2}{G:X2}{B:X2}";
}