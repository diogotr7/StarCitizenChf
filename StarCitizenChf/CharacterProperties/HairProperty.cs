using System;
using System.Diagnostics;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/shared/hair/hair_13.xml
internal sealed class HairProperty
{
    public const uint Key = 0x13601A95;
    public const string KeyRep = "95-1A-60-13";

    public Guid Id { get; set; }
    
    public ulong ChildCount { get; set; }

    public static HairProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        return new HairProperty()
        {
            Id = guid,
            ChildCount = childCount
        };
    }
}