using ChfUtils;

namespace ChfParser;

public sealed class StarCitizenCharacter
{
    public required GenderProperty Gender { get; init; }
    public required DnaProperty Dna { get; init; }
    public required BodyProperty Body { get; init; }
    public required HeadMaterialProperty HeadMaterial { get; init; }
    public required CustomMaterialProperty CustomMaterial { get; init; }
    public required List<UnknownProperty> Props { get; init; }
    public required EyeMaterial EyeMaterial { get; init; }
    public required BodyMaterial BodyMaterial { get; init; }

    public static StarCitizenCharacter FromBytes(ReadOnlySpan<byte> data)
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

        var props = new List<UnknownProperty>();
        while (reader.Peek<uint>() != EyeMaterial.Key)
        {
            props.Add(UnknownProperty.Read(ref reader));
        }

        var eyeMaterial = EyeMaterial.Read(ref reader);
        var bodyMaterialInfo = BodyMaterial.Read(ref reader);

        if (reader.Position != reader.Span.Length)
            throw new Exception($"Unexpected data at the end of the file: {reader.Remaining.Length} bytes");

        return new StarCitizenCharacter
        {
            Gender = gender,
            Dna = dnaProperty,
            Body = body,
            HeadMaterial = headMaterial,
            CustomMaterial = customMaterial,
            EyeMaterial = eyeMaterial,
            BodyMaterial = bodyMaterialInfo,
            Props = props
        };
    }
}