using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class HairProperty
{
    public const uint Key = 0x13601A95;
    public const string KeyRep = "95-1A-60-13";

    public byte[] Data { get; set; }
    
    public uint ChildCount { get; set; }

    public static HairProperty Read(ref SpanReader reader)
    {
        var hair = reader.ReadBytes(sizeof(uint) * 4);
        var childCount = reader.Read<uint>();
        reader.Expect(0);

        return new HairProperty()
        {
            Data = hair.ToArray(),
            ChildCount = childCount
        };
    }
}