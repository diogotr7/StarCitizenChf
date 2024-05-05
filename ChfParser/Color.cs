using System.Runtime.InteropServices;

namespace ChfParser;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct Color
{
    public readonly byte R;
    public readonly byte G;
    public readonly byte B;
    //Alpha seems to be unused. Keep it for alignment.
    private readonly byte _A;
    
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}