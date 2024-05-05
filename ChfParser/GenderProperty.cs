using ChfUtils;

namespace ChfParser;

public sealed class GenderProperty
{
    public required Guid Id { get; init; }
    
    public static GenderProperty Read(ref SpanReader reader)
    {
        var guid = reader.Read<Guid>();
        reader.Expect(Guid.Empty);
        
        return new GenderProperty { Id = guid };
    }
}