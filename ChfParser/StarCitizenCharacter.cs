using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ChfUtils;

namespace ChfParser;

public sealed class StarCitizenCharacter
{
    public required string Name { get; init; }
    public required string Gender { get; init; }
    public required ulong TotalCount { get; init; }
    
    public required string HairId { get; init; }
    public required string HairModId { get; init; }
    public required string EyeBrowId { get; init; }
    public required string BeardId { get; init; }
    public required string BeardModId { get; init; }
    public required string HeadMatId { get; init; }

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
        var headMaterial = HeadMaterialProperty.Read(ref reader);

        //unknownprop 6 or 7. i am completely lost here
        //72129E8E or A5378A05
        //nextCount is always 0

        return new StarCitizenCharacter()
        {
            Name = fileName,
            Gender = GuidUtils.Shorten(gender.Id),
            TotalCount = totalCount,
            
            HairId = GuidUtils.Shorten(body.Head.Hair.Id),
            HairModId = GuidUtils.Shorten(body.Head.Hair?.Modifier?.Id ?? Guid.Empty),
            EyeBrowId = GuidUtils.Shorten(body.Head.Eyebrow?.Id ?? Guid.Empty),
            BeardId = GuidUtils.Shorten(body.Head.FacialHair?.Id ?? Guid.Empty),
            BeardModId = GuidUtils.Shorten(body.Head.FacialHair?.Modifier?.Id ?? Guid.Empty),
            HeadMatId = GuidUtils.Shorten(headMaterial.Id),

            Next = reader.Read<uint>().ToString("X8"),
            NextCount = (ulong)reader.Remaining.Length,
        };
    }
}