using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
public sealed class HeadProperty
{
    public static readonly Guid Head = new("1d5cfab3-bf80-4550-b4ab-39e896a7086e");
    public const uint Key = 0x47010DB9;
    
    public required ulong ChildCount { get; init; }
    public required EyesProperty? Eyes { get; init; }
    public required HairProperty? Hair { get; init; }
    public required EyebrowProperty? Eyebrow { get; init; }
    public required EyelashProperty? Eyelash { get; init; }
    public required FacialHairProperty? FacialHair { get; init; }
    
    public static HeadProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Head);
        
        var childCount = reader.Read<ulong>();
        
        EyesProperty? eyes = null;
        HairProperty? hair = null;
        EyebrowProperty? eyebrow = null;
        EyelashProperty? eyelash = null;
        FacialHairProperty? facialHair = null;
        
        for (var i = 0; i < (int)childCount; i++)
        {
            switch (reader.Peek<uint>())
            {
                case EyesProperty.Key:
                    eyes = EyesProperty.Read(ref reader);
                    break;
                case HairProperty.Key:
                    hair = HairProperty.Read(ref reader);
                    break;
                case EyebrowProperty.Key:
                    eyebrow = EyebrowProperty.Read(ref reader);
                    break;
                case EyelashProperty.Key:
                    eyelash = EyelashProperty.Read(ref reader);
                    break;
                case FacialHairProperty.Key:
                    facialHair = FacialHairProperty.Read(ref reader);
                    break;
                default:
                    throw new Exception();
            }
        }
        
        return new HeadProperty
        {
            ChildCount = childCount,
            Eyes = eyes ?? throw new Exception("EyesProperty is required"),
            Eyelash = eyelash ?? throw new Exception("EyelashProperty is required"),
            Hair = hair ?? new HairProperty { HairType = HairType.None, Modifier = null },
            Eyebrow = eyebrow ?? new EyebrowProperty { EyebrowType = EyebrowType.None, ChildCount = 0 },
            FacialHair = facialHair ?? new FacialHairProperty { FacialHairType = FacialHairType.None, Modifier = null }
        };
    }
}