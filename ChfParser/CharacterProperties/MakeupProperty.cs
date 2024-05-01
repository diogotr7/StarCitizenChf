using ChfUtils;

namespace ChfParser;

//this does not look right.
internal sealed class MakeupProperty
{
    public const uint Key = 0x0C000000;
    public const string KeyRep = "00-00-00-0C";
    
    public required Guid Id { get; init; }
    public required ulong ChildCount { get; init; }
    
    public static MakeupProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new InvalidDataException($"Expected key {KeyRep}, but got {key:X8}");
        
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        return new MakeupProperty()
        {
            Id = guid,
            ChildCount = childCount
        };
    }
}