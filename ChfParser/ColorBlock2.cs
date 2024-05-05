using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class ColorBlock2
{
    public required Color? Color01 { get; init; }
    public required Color? Color02 { get; init; }
    
    public static ColorBlock2 Read(ref SpanReader reader)
    {
        var count = reader.Read<ulong>();
        switch (count)
        {
            case 2:
                var data53 = reader.ReadKeyValueAndChildCount<Color>(0, "14-08-E9-15");
                var data54 = reader.ReadKeyValueAndChildCount<Color>(0, "09-C9-C7-A2");
        
                return new ColorBlock2
                {
                    Color01 = data53,
                    Color02 = data54
                };
            case 1:
                var asd = reader.ReadKeyValueAndChildCount<Color>(0, "AC-34-2A-44");
                
                return new ColorBlock2
                {
                    Color01 = asd,
                    Color02 = null
                };
            case 0:
                return new ColorBlock2
                {
                    Color01 = null,
                    Color02 = null
                };
            default:
                throw new Exception($"Expected 1 or 2 colors, got {count}");
        }
    }
}