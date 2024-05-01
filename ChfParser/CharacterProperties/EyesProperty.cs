using System;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
public sealed class EyesProperty
{
    public const uint Key = 0xC5BB5550;
    public const string KeyRep = "50-55-BB-C5";
    
    public static EyesProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Guid.Parse("6b4ca363-e160-4871-b709-e47467b40310"));
        var childCount = reader.Read<ulong>();
        if (childCount != 0)
            throw new Exception();

        return new EyesProperty();
    }
}