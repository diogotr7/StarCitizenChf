using System.Diagnostics;

namespace ChfParser;

public sealed class DnaProperty
{
    private const int Size = 0xD8;
    private const int PartCount = 48;

    public required string DnaString { get; init; }
    public required uint ChildCount { get; init; }
    public required Dictionary<FacePart, DnaPart[]> Parts { get; init; }

    public static DnaProperty Read(ref SpanReader reader, BodyType bodyType)
    {
        var male = bodyType == BodyType.Male;

        reader.Expect<ulong>(Size);
        var dnaBytes = reader.ReadBytes(Size).ToArray();
        var dnaString = Convert.ToHexString(dnaBytes);

        var childReader = new SpanReader(dnaBytes);
        
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

        var perBodyPart = Enum.GetValues<FacePart>().ToDictionary(x => x, _ => new DnaPart[4]);
        for (var i = 0; i < PartCount; i++)
        {
            perBodyPart[(FacePart)(i % 12)][i / 12] = DnaPart.Read(ref childReader);
        }

        foreach (var (k, v) in perBodyPart)
        {
            if (Math.Abs(v.Sum(x => x.Percent) - 100) > 5f)//it's fiiiine
                throw new Exception($"Invalid part percent for {k}");
        }
        
        return new DnaProperty
        {
            DnaString = dnaString,
            ChildCount = size,
            Parts = perBodyPart
        };
    }
}

[DebuggerDisplay("{HeadId} {Percent}")]
public sealed class DnaPart
{
    public required byte HeadId { get; init; }
    public required float Percent { get; init; }

    public static DnaPart Read(ref SpanReader reader)
    {
        var value = reader.Read<ushort>();
        var headId = reader.Read<byte>();
        reader.Expect<byte>(0);

        return new DnaPart
        {
            Percent = value / (float)ushort.MaxValue * 100f,
            HeadId = headId
        };
    }
}

public enum FacePart
{
    EyebrowLeft = 0,
    EyebrowRight = 1,
    EyeLeft = 2,
    EyeRight = 3,
    Nose = 4,
    EarLeft = 5,
    EarRight = 6,
    CheekLeft = 7,
    CheekRight = 8,
    Mouth = 9,
    Jaw = 10,
    Crown = 11
}