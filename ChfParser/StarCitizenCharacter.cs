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
    
    public required int LastReadIndex { get; set; }
    
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
        var customMaterial = CustomMaterialProperty.Read(ref reader);
        TestParser.Read(ref reader);
        
        return new StarCitizenCharacter()
        {
            Name = fileName,
            Gender = Constants.GetName(gender.Id),
            TotalCount = totalCount,
            HairId = Constants.GetName(body.Head.Hair.Id),
            HairModId = Constants.GetName(body.Head.Hair?.Modifier?.Id ?? Guid.Empty),
            EyeBrowId = Constants.GetName(body.Head.Eyebrow?.Id ?? Guid.Empty),
            BeardId = Constants.GetName(body.Head.FacialHair?.Id ?? Guid.Empty),
            BeardModId = Constants.GetName(body.Head.FacialHair?.Modifier?.Id ?? Guid.Empty),
            LastReadIndex = reader.Position,
        };
    }
}