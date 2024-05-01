using System;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
internal sealed class EyesProperty
{
    public const uint Key = 0xC5BB5550;
    public const string KeyRep = "50-55-BB-C5";
    
    public static EyesProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();

        if (reader.ReadGuid() != Guid.Parse("6b4ca363-e160-4871-b709-e47467b40310"))
            throw new Exception();
        
        var childCount = reader.Read<ulong>();
        if (childCount != 0)
            throw new Exception();

        return new EyesProperty();
    }
}