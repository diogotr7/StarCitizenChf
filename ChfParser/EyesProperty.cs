using System;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/pu/eyes/pu_head_eyes_white_charactercustomizer.xml
public sealed class EyesProperty
{
    public static readonly Guid Eyes = new("6b4ca363-e160-4871-b709-e47467b40310");
    public const uint Key = 0xC5BB5550;
    
    public static EyesProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Eyes);
        reader.Expect<ulong>(0);

        return new EyesProperty();
    }
}