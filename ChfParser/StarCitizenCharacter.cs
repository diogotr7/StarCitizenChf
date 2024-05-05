using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class StarCitizenCharacter
{
    public required string Name { get; init; }
    public required string Gender { get; init; }
    
    public required string DnaString { get; init; }
    public required ulong TotalCount { get; init; }
    
    public required string HairId { get; init; }
    public required string HairModId { get; init; }
    public required string EyeBrowId { get; init; }
    public required string BeardId { get; init; }
    public required string BeardModId { get; init; }
    public required string HeadMaterialId { get; init; }
    public required Color TorsoColor { get; init; }
    public required Color LimbColor { get; init; }
    public required Color EyeColor { get; init; }
    public required Color HeadColor { get; init; }
    
    public required int LastReadIndex { get; init; }
    public required string Special { get; init; }
    
    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var reader = new SpanReader(data);
        
        reader.Expect<uint>(2);
        reader.Expect<uint>(7);

        var gender = GenderProperty.Read(ref reader);
        var dnaProperty = DnaProperty.Read(ref reader);
        var totalCount = reader.Read<ulong>();
        var body = BodyProperty.Read(ref reader);
        var headMaterial = HeadMaterialProperty.Read(ref reader);
        var customMaterial = CustomMaterialProperty.Read(ref reader, headMaterial.Id);
        while (reader.Peek<uint>() != EyeMaterial.Key)
        {
            var nextKey = reader.Read<uint>();
            if (nextKey != 0x6C_83_69_47 && nextKey != 0x07_8A_C8_BD && nextKey != 0x9B_27_4D_93)
                throw new Exception($"Unexpected key: {nextKey:X}");
            
            reader.Expect(Guid.Empty);
            var k = reader.Read<uint>();
            reader.Expect(Guid.Empty);
            reader.Expect(1);
            reader.Expect(5);
            var floatsX = FloatBlock2.Read(ref reader);
            var colorsX = ColorBlock2.Read(ref reader);
            reader.Expect(5);
        }

        var eyeMaterial = EyeMaterial.Read(ref reader);
        var bodyMaterialInfo = BodyMaterialInfo.Read(ref reader);

        return new StarCitizenCharacter
        {
            Name = fileName,
            Gender = Constants.GetName(gender.Id),
            DnaString = dnaProperty.Dna[..8],
            TotalCount = totalCount,
            HairId = Constants.GetName(body.Head.Hair.Id),
            HairModId = Constants.GetName(body.Head.Hair.Modifier?.Id ?? Guid.Empty),
            EyeBrowId = Constants.GetName(body.Head.Eyebrow?.Id ?? Guid.Empty),
            BeardId = Constants.GetName(body.Head.FacialHair?.Id ?? Guid.Empty),
            BeardModId = Constants.GetName(body.Head.FacialHair?.Modifier?.Id ?? Guid.Empty),
            HeadMaterialId = Constants.GetName(headMaterial.Id),
            TorsoColor = bodyMaterialInfo.TorsoColor,
            LimbColor = bodyMaterialInfo.LimbColor,
            EyeColor = eyeMaterial.EyeColor,
            HeadColor = customMaterial.Colors.HeadColor,
            LastReadIndex = reader.Position,
            Special = ""
        };
    }
}