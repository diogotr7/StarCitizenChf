using System;
using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

internal sealed class EyebrowProperty
{
    public const uint Key = 0x1787EE22;
    public const string KeyRep = "22-EE-87-17";
    public Guid Id { get; set; }
    
    public ulong ChildCount { get; set; }

    private static EyebrowProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        return new EyebrowProperty()
        {
            Id = guid,
            ChildCount = childCount
        };
    }
    
    public static EyebrowProperty? ReadOptional(ref SpanReader reader)
    {
        if (reader.PeekKey != Key)
            return null;
        
        _ = reader.Read<uint>();
        
        return Read(ref reader);
    }
}