using System;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
internal sealed class HeadProperty
{
    public const uint Key = 0x47010DB9;
    public const string KeyRep = "B9-0D-01-47";
    
    public ulong ChildCount { get; set; }
    
    public static HeadProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        if (reader.ReadGuid() != Guid.Parse("1d5cfab3-bf80-4550-b4ab-39e896a7086e"))
            throw new Exception();
        
        var childCount = reader.Read<ulong>();

        return new HeadProperty()
        {
            ChildCount = childCount,
        };
    }
}