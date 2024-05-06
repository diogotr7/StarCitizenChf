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

    public required HashSet<Color> AllColors { get; init; }

    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var colors = new HashSet<Color>();
        var reader = new SpanReader(data);

        reader.Expect<uint>(2);
        reader.Expect<uint>(7);

        var gender = GenderProperty.Read(ref reader);
        var dnaProperty = DnaProperty.Read(ref reader);
        var totalCount = reader.Read<ulong>();
        var body = BodyProperty.Read(ref reader);
        var headMaterial = HeadMaterialProperty.Read(ref reader);
        var customMaterial = CustomMaterialProperty.Read(ref reader, headMaterial.Id);

        colors.Add(customMaterial.Colors.HeadColor);
        colors.Add(customMaterial.Colors.Data01);
        colors.Add(customMaterial.Colors.Data02);
        colors.Add(customMaterial.Colors.Data03);
        colors.Add(customMaterial.Colors.Data04);
        colors.Add(customMaterial.Colors.Data05);
        colors.Add(customMaterial.Colors.Data06);
        colors.Add(customMaterial.Colors.Data07);
        colors.Add(customMaterial.Colors.Data08);
        colors.Add(customMaterial.Colors.Data09);

        var props = new List<UnknownProperty>();
        while (reader.Peek<uint>() != EyeMaterial.Key)
        {
            var prop = UnknownProperty.Read(ref reader);

            if (prop.Colors?.Color01 is { } x)
                colors.Add(x);
            if (prop.Colors?.Color02 is { } y)
                colors.Add(y);

            props.Add(prop);
        }

        var eyeMaterial = EyeMaterial.Read(ref reader);
        var bodyMaterialInfo = BodyMaterial.Read(ref reader);

        colors.Add(eyeMaterial.EyeColor);
        colors.Add(bodyMaterialInfo.TorsoColor);
        colors.Add(bodyMaterialInfo.LimbColor);

        if (reader.Position != reader.Span.Length)
            throw new Exception($"Unexpected data at the end of the file: {reader.Remaining.Length} bytes");

        return new StarCitizenCharacter
        {
            Name = Path.GetFileNameWithoutExtension(fileName),
            Gender = Constants.GetName(gender.Id),
            DnaString = dnaProperty.Dna[..8],
            TotalCount = totalCount,
            HairId = Constants.GetName(body.Head.Hair?.Id ?? Guid.Empty),
            HairModId = Constants.GetName(body.Head.Hair?.Modifier?.Id ?? Guid.Empty),
            EyeBrowId = Constants.GetName(body.Head.Eyebrow?.Id ?? Guid.Empty),
            BeardId = Constants.GetName(body.Head.FacialHair?.Id ?? Guid.Empty),
            BeardModId = Constants.GetName(body.Head.FacialHair?.Modifier?.Id ?? Guid.Empty),
            HeadMaterialId = Constants.GetName(headMaterial.Id),
            AllColors = colors
        };
    }
}