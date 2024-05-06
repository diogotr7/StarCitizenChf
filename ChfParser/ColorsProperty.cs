using ChfUtils;

namespace ChfParser;

public sealed class ColorsProperty
{
    public required Color Color01 { get; init; }
    public required Color Color02 { get; init; }
    
    public static ColorsProperty Read(ref SpanReader reader)
    {
        var count = reader.Read<ulong>();
        switch (count)
        {
            case 2:
                var data53 = reader.ReadKeyValueAndChildCount<Color>(0, 0x15e90814);
                var data54 = reader.ReadKeyValueAndChildCount<Color>(0, 0xa2c7c909);
        
                return new ColorsProperty
                {
                    Color01 = data53,
                    Color02 = data54
                };
            case 1:
                var asd = reader.ReadKeyValueAndChildCount<Color>(0, 0x442a34ac);
                
                return new ColorsProperty
                {
                    Color01 = asd,
                    Color02 = new Color(0,0,0)
                };
            case 0:
                return new ColorsProperty
                {
                    Color01 = new Color(0,0,0),
                    Color02 = new Color(0,0,0)
                };
            default:
                throw new Exception($"Expected 1 or 2 colors, got {count}");
        }
    }
}