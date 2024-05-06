using ChfUtils;

namespace ChfParser;

public sealed class EyebrowProperty
{
    public const uint Key = 0x1787EE22;
    
    public required Guid Id { get; init; }
    public required ulong ChildCount { get; init; }
    
    public static EyebrowProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var childCount = reader.Read<ulong>();

        return new EyebrowProperty
        {
            Id = guid,
            ChildCount = childCount
        };
    }
}