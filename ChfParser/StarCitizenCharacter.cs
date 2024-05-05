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
        
        //UNKNOWN START
        //is this useful to us?
        var customMaterial = CustomMaterialProperty.Read(ref reader, headMaterial.Id);
        var floats = FloatBlock.Read(ref reader);
        //these colors seem to be makeup-related. Hair is next?
        var colors = ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);
        
        //from the end of the last read to the end of the file
        var target = reader.Remaining.Length - BodyMaterialInfo.Size - EyeMaterial.Size;
        var idk = reader.ReadBytes(target);
        
        idks.Add((fileName, idk.ToArray()));
        
        //UNKNOWN END
        //this last section seems preety consistent.
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
            BodyColor1 = bodyMaterialInfo.TorsoColor,
            BodyColor2 = bodyMaterialInfo.LimbColor,
            EyeColor = eyeMaterial.EyeColor,
            CustomColor = colors.HeadColor,
            LastReadIndex = reader.Position,
            Special = "asd"
        };
    }
}