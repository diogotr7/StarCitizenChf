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
    public required Color BodyColor1 { get; init; }
    public required Color BodyColor2 { get; init; }
    public required Color EyeColor { get; init; }
    public required Color CustomColor { get; init; }
    
    public required int LastReadIndex { get; init; }
    public required string Special { get; init; }
    
    public static readonly List<(string, byte[])> idks = [];
    
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
        
        //is this useful to us?
        var customMaterial = CustomMaterialProperty.Read(ref reader, headMaterial.Id);
        var floats = FloatBlock.Read(ref reader);
        //these colors seem to be makeup-related. Hair is next?
        var colors = ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);
        
        string asd = "";
        //if we have 47-69-83-6C, read that. It's possible that it's not there.
        //in that case, figure out what other keys might be possible.
        //bd-c8-8a-07
        //93-4d-27-9b
        //5e-88-47-a0

        while (reader.Peek<uint>() != EyeMaterial.Key)
        {
            var nextKey = reader.Read<uint>();
            switch (nextKey)
            {
                case 0x6C_83_69_47:
                    reader.Expect(Guid.Empty);
                    var k1 = reader.Read<uint>();
                    reader.Expect(Guid.Empty);
                    reader.Expect(1);
                    reader.Expect(5);
                    var floats2 = FloatBlock2.Read(ref reader);
                    var colors2 = ColorBlock2.Read(ref reader);
                    reader.Expect(5);
                    
                    break;
                case 0x07_8A_C8_BD:
                    reader.Expect(Guid.Empty);
                    var k2 = reader.Read<uint>();
                    reader.Expect(Guid.Empty);
                    reader.Expect(1);
                    reader.Expect(5);
                    var floats3 = FloatBlock2.Read(ref reader);
                    var colors3 = ColorBlock2.Read(ref reader);
                    reader.Expect(5);
                    
                    break;
                case 0x9B_27_4D_93:
                    reader.Expect(Guid.Empty);
                    var k3 = reader.Read<uint>();
                    reader.Expect(Guid.Empty);
                    reader.Expect(1);
                    reader.Expect(5);
                    var floats4 = FloatBlock2.Read(ref reader);
                    var colors4 = ColorBlock2.Read(ref reader);
                    reader.Expect(5);

                    break;
                default:
                    asd = nextKey.ToString("X8");
                    Console.WriteLine($"Unexpected key: {nextKey:X8}");
                    goto exit;
            }
        }

        exit:

        var eyeMaterial = EyeMaterial.Read(ref reader);
        var bodyMaterialInfo = BodyMaterialInfo.Read(ref reader);
        Debug.Assert(reader.Remaining.Length == 0);

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
            BodyColor1 = new Color(),//bodyMaterialInfo.TorsoColor,
            BodyColor2 = new Color(),//bodyMaterialInfo.LimbColor,
            EyeColor = new Color(),//eyeMaterial.EyeColor,
            CustomColor = colors.HeadColor,
            LastReadIndex = reader.Position,
            Special = asd
        };
    }
}