using ChfUtils;

namespace ChfParser;

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