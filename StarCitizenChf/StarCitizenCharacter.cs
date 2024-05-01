using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StarCitizenChf;

public sealed class StarCitizenCharacter
{
    public required string Name { get; init; }
    public required string Gender { get; init; }
    public required ulong TotalCount { get; init; }
    public required ulong HeadCount { get; init; }
    
    public required string HairId { get; init; }
    public required string HairModId { get; init; }
    public required string HeadMatId { get; init; }
    public required string EyeBrowId { get; init; }
    public required string BeardId { get; init; }
    public required string BeardModId { get; init; }

    public required string Next { get; init; }
    public required ulong NextCount { get; init; }
    
    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var reader = new SpanReader(data);
        
        reader.Expect<uint>(2); //version?
        reader.Expect<uint>(7); //chf version definitely

        var gender = GenderProperty.Read(ref reader);
        var dnaProperty = DnaProperty.Read(ref reader);
        var totalCount = reader.Read<ulong>();
        var body = BodyProperty.Read(ref reader);
        var head = HeadProperty.Read(ref reader);
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
        if (headChildCount != head.ChildCount) throw new Exception();
        
        var headMaterial = HeadMaterialProperty.Read(ref reader);


        //unknownprop 6 or 7. i am completely lost here
        //72129E8E or A5378A05
        //nextCount is always 0

        return new StarCitizenCharacter()
        {
            Name = fileName,
            Gender = GuidUtils.Shorten(gender.Id),
            TotalCount = totalCount,
            HeadCount = head.ChildCount,
            
            HairId = GuidUtils.Shorten(hair.Id),
            HairModId = GuidUtils.Shorten(hair?.Modifier?.Id ?? Guid.Empty),
            HeadMatId = GuidUtils.Shorten(headMaterial.Id),
            EyeBrowId = GuidUtils.Shorten(eyebrow?.Id ?? Guid.Empty),
            BeardId = GuidUtils.Shorten(facialHair?.Id ?? Guid.Empty),
            BeardModId = GuidUtils.Shorten(facialHair?.Modifier?.Id ?? Guid.Empty),

            Next = reader.Read<uint>().ToString("X8"),
            NextCount = diff,
        };
    }
}

internal sealed class GenderProperty
{
    public Guid Id { get; set; }
    
    public static GenderProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        
        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);
        
        return new GenderProperty() { Id = guid };
    }
}