using ChfUtils;

namespace ChfParser;

public sealed class UnknownProperty
{
    public const uint Key1 = 0x6C_83_69_47;
    public const uint Key2 = 0x07_8A_C8_BD;
    public const uint Key3 = 0x9B_27_4D_93;
    
    public required uint Key { get; init; }
    public required uint Unknown { get; init; }
    public required FloatBlock2? Floats { get; init; }
    public required ColorBlock2? Colors { get; init; }
    
    public static UnknownProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key1 && key != Key2 && key != Key3)
            throw new Exception($"Unexpected key: {key:X}");
        
        reader.Expect(Guid.Empty);
        var id = reader.Read<uint>();
        reader.Expect(Guid.Empty);
        reader.Expect(1);
        reader.Expect(5);
        var floats = FloatBlock2.Read(ref reader);
        var colors = ColorBlock2.Read(ref reader);
        reader.Expect(5);
        
        return new UnknownProperty
        {
            Key = key,
            Unknown = id,
            Floats = floats,
            Colors = colors
        };
    }
}