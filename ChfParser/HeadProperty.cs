using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
public sealed class HeadProperty
{
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
        reader.Expect(Constants.Head);
        
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
            Eyes = eyes,
            Hair = hair,
            Eyebrow = eyebrow,
            Eyelash = eyelash,
            FacialHair = facialHair,
        };
    }
}