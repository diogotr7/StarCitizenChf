
namespace ChfParser;

public sealed class EyebrowProperty
{
    public const uint Key = 0x1787EE22;
    
    public required EyebrowType EyebrowType { get; init; }
    public required ulong ChildCount { get; init; }
    
    public static EyebrowProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var childCount = reader.Read<ulong>();
        
        var type = guid switch
        {
            _ when guid == Brows01 => EyebrowType.Brows01,
            _ when guid == Brows02 => EyebrowType.Brows02,
            _ when guid == Brows03 => EyebrowType.Brows03,
            _ when guid == Brows04 => EyebrowType.Brows04,
            _ when guid == Brows05 => EyebrowType.Brows05,
            _ when guid == Brows06 => EyebrowType.Brows06,
            _ => throw new ArgumentOutOfRangeException(nameof(guid), guid, null)
        };

        return new EyebrowProperty
        {
            EyebrowType = type,
            ChildCount = childCount
        };
    }
    
    public static readonly Guid Brows01 = new("89ec0bbc-7daf-4b09-a98d-f8dd8df32305");
    public static readonly Guid Brows02 = new("c40183e4-659c-4e4e-8f96-70b33a3b9d67");
    public static readonly Guid Brows03 = new("6606176a-bfc4-4d24-a40a-b554fcfb8c7e");
    public static readonly Guid Brows04 = new("41a65deb-4a4c-425c-8825-e6d264ecdd4b");
    public static readonly Guid Brows05 = new("a074880a-6df2-4996-89e2-3e204a2790c2");
    public static readonly Guid Brows06 = new("03270dfe-71be-45ee-b51a-fb1dd7e67ba1");
}

public enum EyebrowType
{
    None,
    Brows01,
    Brows02,
    Brows03,
    Brows04,
    Brows05,
    Brows06
}