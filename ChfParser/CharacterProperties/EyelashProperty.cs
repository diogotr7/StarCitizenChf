using System;
using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/shared/eyelashes/head_eyelashes.xml
internal sealed class EyelashProperty 
{
    public const uint Key = 0x_19_0b_04_e2;
    public const string KeyRep = "E2-04-0B-19";
    
    public ulong ChildCount { get; set; }
    
    //fix, terrible
    public static EyelashProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        if (reader.ReadGuid() != Guid.Parse("6217c113-a448-443b-82aa-1bb108ba8e11"))
            throw new Exception();
        
        reader.Expect(0);
        var childCount = reader.Read<uint>();
        //this value seems to be 0 when the character has a beard?
        switch (childCount)
        {
            case 0:
                return new EyelashProperty() { ChildCount = childCount };
            case 3: //TODOOOOOOOOOOO
            case 4:
            case 5:
            case 6:
                reader.Expect<uint>(5);
                //Debugger.Break();
                return new EyelashProperty() { ChildCount = childCount };
            default:
                Debugger.Break();
                throw new Exception();
        }
    }
}