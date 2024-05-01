using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class EyeBrowProperty
{
    public const uint Key = 0x1787EE22;
    public const string KeyRep = "22-EE-87-17";
    public Guid Id { get; set; }
    
    public ulong ChildCount { get; set; }

    public static EyeBrowProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        return new EyeBrowProperty()
        {
            Id = guid,
            ChildCount = childCount
        };
    }
}