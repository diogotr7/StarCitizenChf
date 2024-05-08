using System.Diagnostics;

namespace ChfParser;

public static class DnaDebug
{
    public static void Debug(ReadOnlySpan<byte> bytes, bool male)
    {
        if (bytes.Length != 0xD8)
            throw new Exception();
        
        var reader = new SpanReader(bytes);
        reader.Expect(0xFCD09394);
        reader.Expect(male ? 0xDD6C67F6 : 0x9EF4EB54);
        reader.Expect(male ? 0x65E740D3 : 0x65D75204);
        reader.Expect(0);
        reader.Expect<byte>(0x0c);
        reader.Expect<byte>(0x0);
        reader.Expect<byte>(0x04);
        reader.Expect<byte>(0x0);
        reader.Expect<byte>(0x4);
        reader.Expect<byte>(0x0);
        var size = reader.Read<byte>();
        if (size > 48)//48 = (216 - 24) / 4
            throw new Exception();
        
        reader.Expect<byte>(0x0);
        //this 'parts' seems to be amount of head parts modified?
        //each head part might be a blend of one or more headIds, i think.
        
        //figuring out what each bodypart each chunk corresponds to is hard.
        var parts = new (float percent, byte key)[48];
        for (var i = 0; i < 48; i++)
        {
            var percent = (float)reader.Read<ushort>() / ushort.MaxValue;
            //1-indexed id of the head. top left is 1. last one is 14.
            var headId = reader.Read<byte>();
            //if (headId > 14)
            //    throw new Exception();
            //if zero, empty uhh slot?
            if (headId == 43)
                Debugger.Break();
            
            reader.Expect<byte>(0);
            
            parts[i] = (percent, headId);
            
            //whole head
            //crown
            //ears
            //brows
            //nose
            //mouth
            //jaw
            //cheeks
            Console.WriteLine($"{i}: {percent} {headId}");
        }
        if(size == 0)
            Debugger.Break();
        
        Console.WriteLine($"Size: {size}");
        
        //starting from zero,
        //change every "eye" blend to a bit above 0, say 20%
        //This makes the count go from 0 to 14, which is the amount of heads.
        
    }
}