using ChfUtils;

namespace ChfParser;

public sealed class EyeMaterial
{
    public const uint Key = 0x_A0_47_88_5E;
    public const int Size = 4 * 11 + 16 * 2 + 12;
    
    public required Color EyeColor { get; init; }
    
    public static EyeMaterial Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Guid.Empty);
        reader.Expect(0xCE_9D_F0_55);
        reader.Expect(Guid.Empty);
        reader.Expect(1);
        reader.Expect(5);
        reader.Expect(0x_97_36_C4_4B);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);
        var c1 = reader.ReadKeyValueAndChildCount<Color>(0, "AC-34-2A-44");
        reader.Expect<uint>(5);
        
        return new EyeMaterial
        {
            EyeColor = c1
        };
    }
}