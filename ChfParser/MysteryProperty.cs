using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class MysteryProperty
{
    public required FloatBlock2? FloatBlock { get; init; }
    public required ColorBlock2 ColorBlock { get; init; }
    public required Guid Id0 { get; init; }
    public required Guid Id1 { get; init; }
    
    public static MysteryProperty Read(ref SpanReader reader)
    {
        var floatBlock = FloatBlock2.Read(ref reader);
        var colorBlock = ColorBlock2.Read(ref reader);

        reader.Expect<uint>(5);

        var data79 = reader.ReadKeyAndGuid();//"5E-88-47-A0");
        var data80 = reader.ReadKeyAndGuid();//"55-F0-9D-CE");

        var x = reader.Read<uint>();
        if (x != 1 && x != 2)
            Debugger.Break();
        reader.Expect<uint>(5);
        
        Console.WriteLine(x);
        
        return new MysteryProperty()
        {
            FloatBlock = floatBlock,
            ColorBlock = colorBlock,
            Id0 = data79,
            Id1 = data80
        };
    }
}