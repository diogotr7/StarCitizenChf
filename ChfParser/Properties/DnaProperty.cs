namespace ChfParser;

public sealed class DnaProperty
{
    private const int Size = 0xD8;
    private const int Parts = 4;
    
    public required string DnaString { get; init; }
    public required uint ChildCount { get; init; }
    public required DnaFaceProperty[] FaceParts { get; init; }
    
    public static DnaProperty Read(ref SpanReader reader, BodyType bodyType)
    {
        var male = bodyType == BodyType.Male;
        
        reader.Expect<ulong>(Size);

        var dna = reader.ReadBytes(Size).ToArray();
        
        var childReader = new SpanReader(dna);
        childReader.Expect(0xFCD09394);
        childReader.Expect(male ? 0xDD6C67F6 : 0x9EF4EB54);
        childReader.Expect(male ? 0x65E740D3 : 0x65D75204);
        childReader.Expect(0);
        childReader.Expect<byte>(0x0c);
        childReader.Expect<byte>(0x0);
        childReader.Expect<byte>(0x04);
        childReader.Expect<byte>(0x0);
        childReader.Expect<byte>(0x4);
        childReader.Expect<byte>(0x0);
        
        var size = childReader.Read<byte>();
        childReader.Expect<byte>(0);
        var children = new DnaFaceProperty[Parts];
        
        for (var i = 0; i < Parts; i++)
        {
            children[i] = DnaFaceProperty.Read(ref childReader, bodyType);
        }
        
        var dnaString = BitConverter.ToString(dna).Replace("-", "");
        
        return new DnaProperty
        {
            DnaString = dnaString,
            ChildCount = size,
            FaceParts = children
        };
    }
}

/// <summary>
/// Describes one of the four chunks of face data
/// </summary>
public sealed class DnaFaceProperty
{
    public required DnaChildProperty[] DnaProperties { get; init; }
    
    public static DnaFaceProperty Read(ref SpanReader reader, BodyType bodyType)
    {
        var dnaProperties = new DnaChildProperty[12];
        
        for (var i = 0; i < 12; i++)
        {
            dnaProperties[i] = DnaChildProperty.Read(ref reader, (FacePart)i);
        }
        
        return new DnaFaceProperty
        {
            DnaProperties = dnaProperties
        };
    }
}

public sealed class DnaChildProperty
{
    public required FacePart Part { get; init; }
    public required float Percent { get; init; }
    public required byte HeadId { get; init; }
    
    public static DnaChildProperty Read(ref SpanReader reader, FacePart facePart)
    {
        var value = reader.Read<ushort>();
        var headId = reader.Read<byte>();
        reader.Expect<byte>(0);
        
        return new DnaChildProperty
        {
            Percent = value / (float)ushort.MaxValue * 100f,
            HeadId = headId,
            Part = facePart
        };
    }
}

public sealed class DnaBlend
{
    public required float Percent { get; init; }
    public required byte HeadId { get; init; }
}

public sealed class DnaFaceInfo
{
    public required FacePart Part { get; init; }
    public required DnaBlend BlendA { get; init; }
    public required DnaBlend BlendB { get; init; }
    public required DnaBlend BlendC { get; init; }
    public required DnaBlend BlendD { get; init; }
}

public enum FacePart
{
    EyebrowLeft = 0,
    EyebrowRight,
    EyeLeft,
    EyeRight,
    Nose,
    EarLeft,
    EarRight,
    CheekLeft,
    CheekRight,
    Mouth,
    Jaw,
    Crown
}