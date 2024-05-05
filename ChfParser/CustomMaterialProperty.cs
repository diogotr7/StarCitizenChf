using ChfUtils;

namespace ChfParser;

public sealed class CustomMaterialProperty
{
    public const uint Key = 0x72_12_9E_8E;
    public const uint SpecialKey = 0xa5_37_8a_05;
    
    public required CustomMaterialChildProperty[] Children { get; init; }
    public required FloatBlock Floats { get; init; }
    public required ColorBlock Colors { get; init; }

    public static CustomMaterialProperty Read(ref SpanReader reader, Guid headMaterial)
    {
        //oddity: when the head material is f11, 05-8A-37-A5 is the key.
        //in *all* other cases, 8E-9E-12-72 is the key. The data seems? to be the same.
        reader.Expect(headMaterial == Constants.HeadMaterialF11 ? SpecialKey : Key);
        var childCount = reader.Read<uint>();
        var children = new CustomMaterialChildProperty[childCount];

        for (var i = 0; i < childCount; i++)
        {
            children[i] = CustomMaterialChildProperty.Read(ref reader);
        }
        
        var floats = FloatBlock.Read(ref reader);
        var colors = ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);

        return new CustomMaterialProperty
        {
            Children = children,
            Floats = floats,
            Colors = colors
        };
    }
}