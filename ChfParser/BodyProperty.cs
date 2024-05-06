using ChfUtils;

namespace ChfParser;

//entities/scitem/characters/human.body/body_01_noMagicPocket
public sealed class BodyProperty
{
    public const uint Key = 0xAB6341AC;
    public required HeadProperty Head { get; init; }
    
    public static BodyProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Constants.Body);
        reader.Expect<ulong>(1);
        
        var head = HeadProperty.Read(ref reader);

        return new BodyProperty
        {
            Head = head,
        };
    }
}