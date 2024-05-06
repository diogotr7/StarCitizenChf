using ChfUtils;

namespace ChfParser;

public sealed class FacialHairProperty
{
    public const uint Key = 0x98EFBB1C;

    public required Guid Id { get; init; }
    public required HairModifierProperty? Modifier { get; init; }
            
    public static FacialHairProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var count = reader.Read<uint>();
        switch (count)
        {
            case 0:
                var cnt = reader.Read<uint>();
                if (cnt != 5 && cnt != 6)
                    throw new Exception();
                
                reader.Expect(5);
                return new FacialHairProperty { Id = guid, Modifier = null };
            case 1:
                reader.Expect<uint>(0);
                
                var hairModifier = HairModifierProperty.Read(ref reader);
                
                return new FacialHairProperty() { Id = guid, Modifier = hairModifier};
            default:
                throw new Exception();
        }
    }
}