using System;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
public sealed class HeadProperty
{
    public const uint Key = 0x47010DB9;
    
    public required ulong ChildCount { get; init; }
    public required EyesProperty Eyes { get; init; }
    public required HairProperty Hair { get; init; }
    public required EyebrowProperty? Eyebrow { get; init; }
    public required EyelashProperty Eyelash { get; init; }
    public required FacialHairProperty? FacialHair { get; init; }
    
    public static HeadProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Constants.Head);
        
        var childCount = reader.Read<ulong>();
        
        var eyes = EyesProperty.Read(ref reader);
        var hair = HairProperty.Read(ref reader);
        var eyebrow = EyebrowProperty.ReadOptional(ref reader);
        var eyelash = EyelashProperty.Read(ref reader);
        var facialHair = FacialHairProperty.ReadOptional(ref reader);

        ulong headChildCount = 3;
        if (eyebrow != null) headChildCount++;
        if (facialHair != null) headChildCount++;
        if (headChildCount != childCount) throw new Exception();
        
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