using System;
using System.Diagnostics;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/head/shared/eyelashes/head_eyelashes.xml
internal sealed class EyelashProperty 
{
    public const uint Key = 0x_19_0b_04_e2;
    public const string KeyRep = "E2-04-0B-19";
    
    public ulong ChildCount { get; set; }
    
    //fix, terrible
    public static EyelashProperty Read(ref SpanReader reader)
    {
        if (reader.ReadGuid() != Guid.Parse("6217c113-a448-443b-82aa-1bb108ba8e11"))
            throw new Exception();
        reader.Expect(0);
        var childCount = reader.Read<uint>();//0, 4, 5, 6
        //this value seems to be 0 when the character has a beard?
        switch (childCount)
        {
            case 0:
                return new EyelashProperty() { ChildCount = childCount };
            case 4:
            case 5:
            case 6:
            case 3: //TODOOOOOOOOOOO
                reader.Expect<uint>(5);
                return new EyelashProperty() { ChildCount = childCount };
            default:
                Console.WriteLine("problem");
                return new EyelashProperty() { ChildCount = childCount };
                break;
        }

        throw new Exception();
    }
}