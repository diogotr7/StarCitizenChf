using System;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
public sealed class EyesProperty
{
    public const uint Key = 0xC5BB5550;
    
    public static EyesProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Constants.Eyes);
        reader.Expect<ulong>(0);

        return new EyesProperty();
    }
}