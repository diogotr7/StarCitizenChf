using System;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
internal sealed class HeadProperty
{
    public const uint Key = 0x47010DB9;
    public const string KeyRep = "B9-0D-01-47";
    
    public required ulong ChildCount { get; init; }
    public required EyesProperty Eyes { get; init; }
    public required HairProperty Hair { get; init; }
    public required EyebrowProperty? Eyebrow { get; init; }
    public required EyelashProperty Eyelash { get; init; }
    public required FacialHairProperty? FacialHair { get; init; }
    
    public static HeadProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        if (reader.ReadGuid() != Guid.Parse("1d5cfab3-bf80-4550-b4ab-39e896a7086e"))
            throw new Exception();
        
        var childCount = reader.Read<ulong>();
        var eyes = EyesProperty.Read(ref reader);
        var hair = HairProperty.Read(ref reader);
        var eyebrow = EyebrowProperty.ReadOptional(ref reader);
        var eyelash = EyelashProperty.Read(ref reader);
        var facialHair = FacialHairProperty.ReadOptional(ref reader);
        
        ulong headChildCount = 0;
        if (eyes != null) headChildCount++;
        if (hair != null) headChildCount++;
        if (eyebrow != null) headChildCount++;
        if (eyelash != null) headChildCount++;
        if (facialHair != null) headChildCount++;
        if (headChildCount != childCount) throw new Exception();
        
        return new HeadProperty()
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