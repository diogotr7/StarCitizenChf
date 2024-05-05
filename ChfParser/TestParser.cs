using ChfUtils;

namespace ChfParser;

public static class TestParser
{
    public static string Read(ref SpanReader reader)
    {
        var floatBlock1 = FloatBlock.Read(ref reader);
        var colorBlock1 = ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);
        switch (reader.NextKey)
        {
            case "47-69-83-6C":
                reader.ReadBytes(4);//key
                reader.Expect(Guid.Empty);
                var k2 = reader.Read<uint>();
                reader.Expect(Guid.Empty);
                reader.Expect<uint>(1);
                reader.Expect<uint>(5);
                var m1 = MysteryProperty.Read(ref reader);
                var m2 = MysteryProperty.Read(ref reader);
                var m3 = MysteryProperty.Read(ref reader);
                // var floatBlock2 = FloatBlock2.Read(ref reader);
                // var colorBlock2 = ColorBlock2.Read(ref reader);
                return reader.NextKey;
                
                var next1 = reader.ReadUntil([0x93, 0x4D, 0x27, 0x9B]);
                if (next1.Length == 0)
                {
                    //the next block is not 93-4D-27-9B. Try to find 5E-88-47-A0
                    var next3 = reader.ReadUntil([0x5E, 0x88, 0x47, 0xA0]);
                    break;
                }
                
                break;
            case "93-4D-27-9B":
                var next2 = reader.ReadUntil([0x5E, 0x88, 0x47, 0xA0]);
                break;
            //BD-C8-8A-07
            case "5E-88-47-A0":
                //yay!
                break;
            default:
                throw new Exception($"Unexpected key: {reader.NextKey}");
        }
        
        //if nextkey is "47-69-83-6C", read that. usually 368 bytes
        //if nextkey is "93-4D-27-9B", read that. can be 76 or 184 bytes??
        //if nextkey is not "5E-88-47-A0", throw?

        return "";
        //
        // if (reader.NextKey == "93-4D-27-9B" || reader.NextKey == "5E-88-47-A0")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // //the above ones seem to be extremely rare, so we can ignore them for now.
        //
        // //zero guids
        // var data44 = reader.ReadKeyAndGuid("47-69-83-6C");
        // var data45 = reader.ReadKeyAndGuid();
        //
        // reader.Expect<uint>(1);
        // reader.Expect<uint>(5);
        //
        // //-608, -240, -424
        // if (reader.NextKey == "4F-9C-58-9B")
        // {
        //     var g1 = reader.ReadKeyAndGuid();
        //     reader.Expect<uint>(0);
        //     reader.Expect<uint>(5);
        //     var g2 =reader.ReadKeyAndGuid();//93-4D-27-9B
        //     var g3 =reader.ReadKeyAndGuid();//61-70-55-C4
        //     reader.Expect<uint>(1);
        //     reader.Expect<uint>(5);
        //     var mm1 = MysteryProperty.Read(ref reader);
        //     var mm2 = MysteryProperty.Read(ref reader);
        //     var u1 = reader.ReadKeyValueAndChildCount<uint>(0);
        //     reader.Expect<uint>(0);
        //     var cl2 = ColorBlock2.Read(ref reader);
        //     reader.Expect<uint>(5);
        //     var bodymat = BodyMaterialProperty.Read(ref reader);
        //     //var g6 = reader.ReadKeyAndGuid();
        //     //var additional = reader.Read<uint>();
        //     //reader.Expect<uint>(0);
        //     //reader.Expect<uint>(0);
        //     //reader.Expect<uint>(0);
        //     //reader.Expect<uint>(0);
        //     //reader.Expect<uint>(2);
        //     //reader.Expect<uint>(5);
        //     var u2 = reader.ReadKeyValueAndChildCount<uint>(0);
        //     reader.Expect<uint>(0);
        //     reader.Expect<uint>(1);
        //     reader.Expect<uint>(0);
        //     var cl3 = reader.ReadKeyValueAndChildCount<Color>(0, "97-07-53-BD");
        //     reader.Expect(5);
        //     var u3 = reader.ReadKeyValueAndChildCount<uint>(0);
        //     reader.Expect(0);
        //     reader.Expect(1);
        //     reader.Expect(0);
        //     var cl4 = reader.ReadKeyValueAndChildCount<Color>(0, "97-07-53-BD");
        //     
        //     if (reader.Remaining.Length > 0)
        //     {
        //         Console.WriteLine($"Remaining: {reader.Remaining.Length}");
        //     }
        //     
        //     return;
        // }
        // return;
        //
        // //-424
        // if (reader.NextKey == "62-2E-98-67")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // //-532
        // if (reader.NextKey == "9B-31-92-87")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // if (reader.NextKey == "17-AD-1F-1F")
        // {
        //     //key
        //     //0
        //     //7
        //     //5a-c1-f6-4a
        //     //var anotherFloatBlock = MysteryProperty.Read(ref reader);
        //     //Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // var m1 = MysteryProperty.Read(ref reader);
        // var m2 = MysteryProperty.Read(ref reader);
        // var m3 = MysteryProperty.Read(ref reader);
        //
        // if (reader.NextKey == "A9-79-C9-73")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // var data81 = reader.ReadKeyValueAndChildCount<uint>(0, "4B-C4-36-97", "4C-6E-6B-31");
        //
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(1);
        // reader.Expect<uint>(0);
        //
        // var data82 = reader.ReadKeyValueAndChildCount<Color>(0, "AC-34-2A-44", "97-07-53-BD");
        //
        // reader.Expect<uint>(5);
        //
        // if (reader.NextKey == "DB-66-5B-8A")
        // {
        //     Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
        //     return;
        // }
        //
        // reader.ExpectBytes("58-4D-42-27");
        // var someguid = reader.Read<Guid>();
        // var additioonal = reader.Read<uint>();
        //
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(2);
        // reader.Expect<uint>(5);
        //
        // var data83 = reader.ReadKeyValueAndChildCount<uint>(0, "A9-79-C9-73", "4C-6E-6B-31");
        //
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(1);
        // reader.Expect<uint>(0);
        //
        // var data84 = reader.ReadKeyValueAndChildCount<Color>(0, "97-07-53-BD");
        //
        // reader.Expect<uint>(5);
        //
        // var data85 = reader.ReadKeyValueAndChildCount<uint>(0, "2C-A1-1F-A4", "DB-66-5B-8A");
        //
        // reader.Expect<uint>(0);
        // reader.Expect<uint>(1);
        // reader.Expect<uint>(0);
        //
        // var data86 = reader.ReadKeyValueAndChildCount<Color>(0, "97-07-53-BD");
    }
}