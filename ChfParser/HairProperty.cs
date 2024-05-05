using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/shared/hair/hair_13.xml
public sealed class HairProperty
{
    public const uint Key = 0x13601A95;

    public required Guid Id { get; init; }
    public required HairModifierProperty? Modifier { get; init; }
    
    public static HairProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var childCount = reader.Read<ulong>();

        switch (childCount)
        {
            case 0: 
                return new HairProperty { Id = guid, Modifier = null };
            case 1:
            {
                var hairModifier = HairModifierProperty.Read(ref reader);
                if (hairModifier.ChildCount != 0) 
                    throw new Exception("HairModifierProperty child count is not 0");
                
                return new HairProperty
                {
                    Id = guid,
                    Modifier = hairModifier
                };
            }
            default: throw new Exception("HairProperty child count is not 0 or 1");
        }
    }
}