using System;
using ChfUtils;

namespace ChfParser;

internal sealed class GenderProperty
{
    public Guid Id { get; set; }
    
    public static GenderProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        
        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);
        
        return new GenderProperty() { Id = guid };
    }
}