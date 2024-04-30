using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class UnknownProperty7
{
    public const uint Key = 0x_A5_37_8A_05;
    public const string KeyRep = "05-8A-37-A5";
    
    public static UnknownProperty7 Read(ref SpanReader reader)
    {
        var count = reader.Read<uint>();//3,4,5
        reader.Expect(0);
        reader.Expect<uint>(4);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x00000900);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        var unk = reader.Read<uint>();//zero or 0x00190000;
        switch (unk)
        {
            case 0:
                return new UnknownProperty7();
            case 0x00190000:
                return new UnknownProperty7();
            default:
                Debugger.Break();
                break;
        }
        
        throw new Exception();
    }
}