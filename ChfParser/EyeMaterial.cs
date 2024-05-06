using ChfUtils;

namespace ChfParser;

public sealed class EyeMaterial
{
    public const uint Key = 0xA047885E;
    
    public required ColorsProperty EyeColors { get; init; }
    
    public static EyeMaterial Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Guid.Empty);
        reader.Expect(0xCE9DF055);
        reader.Expect(Guid.Empty);
        reader.Expect(1);
        reader.Expect(5);
        reader.Expect(0x9736C44B);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        var colorBlock = ColorsProperty.Read(ref reader);
        reader.Expect<uint>(5);
        
        return new EyeMaterial
        {
            EyeColors = colorBlock
        };
    }
}