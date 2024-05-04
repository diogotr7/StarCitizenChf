using ChfUtils;

namespace ChfParser;

public static class TestParser
{
    public static void Read(ref SpanReader reader)
    {
        var floatBlock1 = FloatBlock.Read(ref reader);
        var colocBlock1 = ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);

        // if (reader.NextKey == "93-4D-27-9B" || reader.NextKey == "5E-88-47-A0")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }

        //the above ones seem to be extremely rare, so we can ignore them for now.

        //zero guids
        var data44 = reader.ReadGuid("47-69-83-6C");
        var data45 = reader.ReadGuid();

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        //-608, -240, -424
        if (reader.NextKey == "4F-9C-58-9B")
        {
            //value of 4f is empty guid
            //0
            //5
            //bd-c8-8a-07 = guid
            //b0-7c-36-91 = guid
            //1
            //5
            
            //comment: this block could be a mystery property, unsure
            //floatblock2
            //colorblock2
            //5
            //5e-88-47-a0 = guid
            //55-f0-9d-ce = guid
            //1
            //5
            
            
            //4b-c4-36-97 = uint
            //0
            //{colorblock2, where count is 1
            //1
            //0
            //ac-34-2a-44 = color
            //}
            //58-4d-42-27 = material, guid + additional uint
            //0
            //0
            //0
            //0
            //2
            //5
            //a9-79-c9-73 = uint
            //0
            //1
            //0
            //97-07-53-bd = color
            //5
            //2c-a1-1f-a4 = uint
            //0
            //1
            //0
            //97-07-53-bd = color
            //end
            
            
            Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
            return;
        }
        
        //-424
        if (reader.NextKey == "62-2E-98-67")
        {
            Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
            return;
        }
        
        //-532
        if (reader.NextKey == "9B-31-92-87")
        {
            Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
            return;
        }
        
        // if (reader.NextKey == "17-AD-1F-1F")
        // {
        //     //key
        //     //0
        //     //7
        //     //5a-c1-f6-4a
        //     var anotherFloatBlock = MysteryProperty.Read(ref reader);
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }

        var m1 = MysteryProperty.Read(ref reader);
        var m2 = MysteryProperty.Read(ref reader);
        var m3 = MysteryProperty.Read(ref reader);

        var data81 = reader.Read<uint>(0, "4B-C4-36-97", "4C-6E-6B-31");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data82 = reader.Read<Color>(0, "AC-34-2A-44", "97-07-53-BD");

        reader.Expect<uint>(5);

        reader.ExpectBytes("58-4D-42-27");
        var someguid = reader.Read<Guid>();
        var additioonal = reader.Read<uint>();

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(2);
        reader.Expect<uint>(5);

        var data83 = reader.Read<uint>(0, "A9-79-C9-73", "4C-6E-6B-31");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data84 = reader.Read<Color>(0, "97-07-53-BD");

        reader.Expect<uint>(5);

        var data85 = reader.Read<uint>(0, "2C-A1-1F-A4", "DB-66-5B-8A");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data86 = reader.Read<Color>(0, "97-07-53-BD");
    }
}