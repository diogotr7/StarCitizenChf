using System;
using ChfUtils;

namespace ChfParser;

public sealed class GenderProperty
{
    public Guid Id { get; set; }
    
    public static GenderProperty Read(ref SpanReader reader)
    {
        var guid = reader.Read<Guid>();
        
        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);
        
        return new GenderProperty() { Id = guid };
    }
}