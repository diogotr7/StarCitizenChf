using ChfUtils;

namespace ChfParser;

//this does not look right.
public sealed class MakeupProperty
{
    public const uint Key = 0x0C000000;
    public const string KeyRep = "00-00-00-0C";
    
    public required Guid Id { get; init; }
    public required ulong ChildCount { get; init; }
    
    public static MakeupProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var childCount = reader.Read<ulong>();

        return new MakeupProperty()
        {
            Id = guid,
            ChildCount = childCount
        };
    }
}