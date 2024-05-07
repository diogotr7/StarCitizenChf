using System.Text.Json.Serialization;

namespace ChfParser;

public sealed class DyeProperty
{
    public required DyeType DyeType { get; init; }
    [JsonConverter(typeof(HexStringJsonConverter))]
    public required uint Unknown { get; init; }
    public required DyeValuesProperty? Values { get; init; }
    public required Color? RootDyeColor { get; init; }
    public required Color? TipDyeColor { get; init; }
    
    public static DyeProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        var dyeType = key switch
        {
            0x6C836947 => DyeType.Hair,
            0x078AC8BD => DyeType.Eyebrow,
            0x9B274D93 => DyeType.Beard,
            _ => throw new Exception($"Unexpected key: {key:X}")
        };
        
        reader.Expect(Guid.Empty);
        var id = reader.Read<uint>();
        reader.Expect(Guid.Empty);
        reader.Expect(1);
        reader.Expect(5);
        var floats = DyeValuesProperty.Read(ref reader);
        var colors = ColorsProperty.Read(ref reader);
        reader.Expect(5);
        
        return new DyeProperty
        {
            DyeType = dyeType,
            Unknown = id,
            Values = floats,
            RootDyeColor = colors.Color02,
            TipDyeColor = colors.Color01
        };
    }
}

public enum DyeType
{
    Hair,
    Eyebrow,
    Beard,
}