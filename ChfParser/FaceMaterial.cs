
namespace ChfParser;

public sealed class FaceMaterial
{
    public const uint Key = 0x72129E8E;
    public const uint SpecialKey = 0xa5378a05;
    
    public required Makeup[] Children { get; init; }
    public required FaceValues Values { get; init; }
    public required FaceColors Colors { get; init; }

    public static FaceMaterial Read(ref SpanReader reader, HeadMaterialType headMaterial)
    {
        //oddity: when the head material is f11, 05-8A-37-A5 is the key.
        //in *all* other cases, 8E-9E-12-72 is the key. The data seems? to be the same.
        reader.Expect(headMaterial == HeadMaterialType.HeadMaterialF11 ? SpecialKey : Key);
        var childCount = reader.Read<uint>();
        var children = new Makeup[childCount];

        for (var i = 0; i < childCount; i++)
        {
            children[i] = Makeup.Read(ref reader);
        }
        
        var floats = FaceValues.Read(ref reader);
        var colors = FaceColors.Read(ref reader);
        reader.Expect<uint>(5);

        return new FaceMaterial
        {
            Children = children,
            Values = floats,
            Colors = colors
        };
    }
}