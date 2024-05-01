using System;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
internal sealed class EyesProperty
{
    public const uint Key = 0xC5BB5550;
    public const string KeyRep = "50-55-BB-C5";
    
    public Guid Id { get; set; }

    public static EyesProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
        var childCount = reader.Read<ulong>();
        
        if (childCount != 0)
            throw new Exception();

        return new EyesProperty()
        {
            Id = guid,
        };
    }
}