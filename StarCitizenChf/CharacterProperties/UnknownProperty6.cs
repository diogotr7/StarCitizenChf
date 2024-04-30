using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class UnknownProperty6
{
    public const uint Key = 0x72129e8e;
    public const string KeyRep = "8E-9E-12-72";
    
    public static UnknownProperty6 Read(ref SpanReader reader)
    {
        //I have no idea at all what to do here, misaligned :(

        //return new UnknownProperty6();
        var count = reader.Read<uint>();
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
                return new UnknownProperty6();
            case 0x00190000:
                return new UnknownProperty6();
            default:
                Debugger.Break();
                break;
        }
        
        throw new Exception();
    }
}