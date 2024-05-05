using System;
using ChfUtils;

namespace ChfParser;

public sealed class GenderProperty
{
    public Guid Id { get; set; }
    
    public static GenderProperty Read(ref SpanReader reader)
    {
        var guid = reader.Read<Guid>();
        reader.Expect<Guid>(Guid.Empty);
        
        return new GenderProperty() { Id = guid };
    }
}