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
                break;
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
                Debugger.Break();
                throw new Exception($"Expected 1 or 2 colors, got {count}");
        }
    }
}

public sealed class Poggers
{
    public const string KeyRep = "74-B6-7E-02";
    public const uint Key = 0x_02_7E_B6_74;
    
    public required FloatBlock2 Flt { get; init; }
    public required ColorBlock2 Clr { get; init; }
    
    public static Poggers Read(ref SpanReader reader)
    {
        var flt = FloatBlock2.Read(ref reader);
        var clr = ColorBlock2.Read(ref reader);
        reader.Expect(0);
        reader.Expect(5);
        
        return new Poggers
        {
            Flt = flt,
            Clr = clr
        };
    }
}