using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/shared/eyelashes/head_eyelashes.xml
public sealed class EyelashProperty 
{
    public const uint Key = 0x_19_0b_04_e2;
    
    public required ulong ChildCount { get; init; }
    
    //fix, terrible
    public static EyelashProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Constants.Eyelashes);
        reader.Expect(0);
        
        var childCount = reader.Read<uint>();
        //this value seems to be 0 when the character has a beard?
        switch (childCount)
        {
            case 0:
                return new EyelashProperty { ChildCount = childCount };
            case 3:
            case 4:
            case 5:
            case 6:
                reader.Expect<uint>(5);
                //Debugger.Break();
                return new EyelashProperty { ChildCount = childCount };
            default:
                Debugger.Break();
                throw new Exception();
        }
    }
}