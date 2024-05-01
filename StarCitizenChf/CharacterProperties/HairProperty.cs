using System;
using System.Diagnostics;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/shared/hair/hair_13.xml
internal sealed class HairProperty
{
    public const uint Key = 0x13601A95;
    public const string KeyRep = "95-1A-60-13";

    public Guid Id { get; init; }
    public HairModifierProperty? Modifier { get; init; }
    
    public static HairProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        switch (childCount)
        {
            case 0: return new HairProperty() { Id = guid };
            case 1:
            {
                var hairModifier = HairModifierProperty.Read(ref reader);
                return new HairProperty()
                {
                    Id = guid,
                    Modifier = hairModifier
                };
            }
            default: throw new Exception();
        }
    }
}