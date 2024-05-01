namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/npc/face/pu_protos_head.xml
internal sealed class HeadProperty
{
    public const uint Key = 0x47010DB9;
    public const string KeyRep = "B9-0D-01-47";
    
    public ulong ChildCount { get; set; }

    public static HeadProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();

        return new HeadProperty()
        {
            ChildCount = childCount
        };
    }
}