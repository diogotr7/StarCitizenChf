namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
internal sealed class EyesProperty
{
    public const uint Key = 0xC5BB5550;
    public const string KeyRep = "50-55-BB-C5";
    
    public uint ChildCount { get; set; }

    public static EyesProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var childCount = reader.Read<uint>();
        reader.Expect(0);

        return new EyesProperty()
        {
            ChildCount = childCount
        };
    }
}