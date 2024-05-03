using System;
using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";
    
    public Guid Id { get; set; }
    
    private static List<string> stupid = new();
    private static List<int> stupid2 = new();
    
    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var additionalParams = reader.Read<uint>();

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);
        reader.ReadBytes(4);//reader.ExpectBytes("8E-9E-12-72");//or "05-8A-37-A5"
        var important = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(4);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("00-09-00-00");
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<ushort>(0);
        //IMPORTANT: THIS NEEDS TO BE A SHORT. EVERYTHING AFTER THIS IS MISALIGNED?


        //this "important" count might tell us how many additional things to read here?
        //If it's 2, the logic in TestParser.Read seems to work well enough. Otherwise, I'm guessing it 
        //tries to read some count too early, unsure.

        ReadOnlySpan<byte> magic = [0xE2,0x27, 0x77, 0xE8];
        var offset = reader.Remaining.IndexOf(magic);
        var predicted = important switch
        {
            2 => 8,
            3 => 29,
            4 => 50,
            5 => 71,
            _ => 0,
        };
        if (offset != predicted)
        {
            Debugger.Break();
        }
        
        //in one of my tests, this 25 appears 33 bytes later than expected which is scary because it seems misaligned.
        stupid2.Add(offset);
        stupid.Add(important.ToString());
        //this following line tries to predict where the next block starts, hopefully it's correct.
        reader.ReadBytes(predicted - 8);
        
        TestParser.Read(ref reader);
        
        Console.WriteLine($"Important: {important}");
        Console.WriteLine($"Offset: {offset}");
        Console.WriteLine($"Predicted: {predicted}");
        Console.WriteLine();

        return new HeadMaterialProperty() { Id = guid };
    }
}