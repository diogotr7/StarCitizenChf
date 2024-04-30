using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class UnknownProperty3 
{
    public const uint Key = 0x_19_0b_04_e2;
    public const string KeyRep = "E2-04-0B-19";
    
    public uint Value { get; set; }
    
    public static UnknownProperty3 Read(ref SpanReader reader)
    {
        reader.ExpectBytes("3B-44-48-A4");
        reader.ExpectBytes("13-C1-17-62");
        reader.ExpectBytes("11-8E-BA-08");
        reader.ExpectBytes("B1-1B-AA-82");
        reader.Expect<uint>(0);
        var count3 = reader.Read<uint>();//0, 4, 5, 6
        //this value seems to be 0 when the character has a beard?
        switch (count3)
        {
            case 0:
                return new UnknownProperty3() { Value = count3 };
            case 4:
            case 5:
            case 6:
                reader.Expect<uint>(5);
                return new UnknownProperty3() { Value = count3 };
            default:
                Debugger.Break();
                break;
        }

        throw new Exception();
    }
}