using ChfUtils;

namespace ChfParser;

//Below this is whatever appears after bodymaterial in the smallest files.
//the data is different, but the key-data-childcount pattern is common.
//sometimes the data is a color, integer or float, in which case the block is 12 bytes.
//some other times, the data is a guid, in which case the block is 16 + 4 + 4 = 24 bytes.
public static class TestParser
{
    private static T Read<T>(ref SpanReader reader, string key, int count = 0) where T : unmanaged
    {
        reader.ExpectBytes(key);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }
    
    public static void Read(ref SpanReader reader)
    {
        var freckleAmount = Read<float>(ref reader, "E2-27-77-E8", 0);
        var freckleOpacity = Read<float>(ref reader, "58-CB-61-93", 0);
        var sunSpotsAmount = Read<float>(ref reader, "0F-D2-4A-55", 0);
        var sunSpotOpacity = Read<float>(ref reader, "64-12-C4-CF", 0);
        
        var data00 = Read<float>(ref reader, "B0-83-58-B9", 0);
        var data01 = Read<uint>(ref reader, "C3-50-F7-9C", 0);
        var data02 = Read<uint>(ref reader, "DF-44-06-A9", 0);
        var data04 = Read<float>(ref reader, "87-A9-71-C8", 0);
        var data05 = Read<float>(ref reader, "F4-7A-DE-ED", 0);
        var data06 = Read<float>(ref reader, "E8-6E-2F-D8", 0);
        var data07 = Read<float>(ref reader, "BA-26-E5-CA", 0);
        var data08 = Read<uint>(ref reader, "02-ED-26-05", 0);
        var data09 = Read<uint>(ref reader, "71-3E-89-20", 0);
        var data10 = Read<uint>(ref reader, "6D-2A-78-15", 0);
        var data11 = Read<float>(ref reader, "D7-D5-E3-9B", 0);
        var data12 = Read<float>(ref reader, "A4-06-4C-BE", 0);
        var data13 = Read<float>(ref reader, "B8-12-BD-8B", 0);
        var data14 = Read<float>(ref reader, "D3-A1-A1-11", 0);
        var data15 = Read<uint>(ref reader, "C3-1A-57-92", 0);
        var data16 = Read<uint>(ref reader, "B0-C9-F8-B7", 0);
        var data17 = Read<uint>(ref reader, "AC-DD-09-82", 0);
        var data18 = Read<float>(ref reader, "E7-01-92-AA", 0);
        var data19 = Read<float>(ref reader, "94-D2-3D-8F", 0);
        var data20 = Read<float>(ref reader, "88-C6-CC-BA", 0);
        var data21 = Read<float>(ref reader, "F4-DC-9D-58", 0);

        reader.Expect<uint>(22);
        reader.Expect<uint>(0);
        
        var data22 = Read<ScColor>(ref reader, "97-07-53-BD", 0);
        var data23 = Read<ScColor>(ref reader, "90-1D-9B-B2", 0);
        var data24 = Read<ScColor>(ref reader, "2F-0E-23-E3", 0);
        var data25 = Read<ScColor>(ref reader, "36-E7-C0-2E", 0);
        var data26 = Read<ScColor>(ref reader, "93-1A-08-1A", 0);
        var data27 = Read<ScColor>(ref reader, "2C-09-B0-4B", 0);
        var data28 = Read<ScColor>(ref reader, "35-E0-53-86", 0);
        var data29 = Read<ScColor>(ref reader, "92-E7-86-7D", 0);
        var data30 = Read<ScColor>(ref reader, "2D-F4-3E-2C", 0);
        var data31 = Read<ScColor>(ref reader, "34-1D-DD-E1", 0);
        var data32 = Read<uint>(ref reader, "EC-83-A5-64", 0);
        var data33 = Read<uint>(ref reader, "18-70-F5-77", 0);
        var data34 = Read<uint>(ref reader, "98-E5-F3-E9", 0);
        var data35 = Read<uint>(ref reader, "6C-16-A3-FA", 0);
        var data36 = Read<uint>(ref reader, "F2-79-B3-3C", 0);
        var data37 = Read<uint>(ref reader, "06-8A-E3-2F", 0);
        var data38 = Read<uint>(ref reader, "F1-62-B7-32", 0);
        var data39 = Read<uint>(ref reader, "05-91-E7-21", 0);
        var data40 = Read<uint>(ref reader, "57-02-E5-F7", 0);
        var data41 = Read<uint>(ref reader, "A3-F1-B5-E4", 0);
        var data42 = Read<uint>(ref reader, "D6-1F-8B-7B", 0);
        var data43 = Read<uint>(ref reader, "22-EC-DB-68", 0);

        reader.Expect<uint>(5);

        var data44 = Read<uint>(ref reader, "47-69-83-6C", 0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        
        var data45 = Read<uint>(ref reader, "8A-CE-74-07", 0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data46 = Read<uint>(ref reader, "17-AD-1F-1F", 7);

        reader.Expect(0);

        var data47_ = Read<uint>(ref reader, "5A-C1-F6-4A", 0);
        var data47 = Read<float>(ref reader, "D9-0B-37-C3", 0);
        var data48 = Read<float>(ref reader, "A3-00-FA-B9", 0);
        var data49 = Read<float>(ref reader, "AF-F0-FB-62", 0);
        var data50 = Read<uint>(ref reader, "76-40-08-06", 0);
        var data51 = Read<float>(ref reader, "C8-A7-9A-A5", 0);
        var data52 = Read<uint>(ref reader, "74-B6-7E-02", 0);

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        var data53 = Read<ScColor>(ref reader, "14-08-E9-15", 0);
        var data54 = Read<ScColor>(ref reader, "09-C9-C7-A2", 0);
        reader.Expect<uint>(5);
        var data55 = Read<uint>(ref reader, "93-4D-27-9B", 0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        var data56 = Read<uint>(ref reader, "A9-5C-56-AC", 0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);
        
        var data57 = Read<uint>(ref reader, "9B-31-92-87", 7);
        reader.Expect<uint>(0);
        
        var data58 = Read<float>(ref reader, "5A-C1-F6-4A", 0);
        var data59 = Read<float>(ref reader, "D9-0B-37-C3", 0);
        var data60 = Read<float>(ref reader, "A3-00-FA-B9", 0);
        var data61 = Read<float>(ref reader, "AF-F0-FB-62", 0);
        var data62 = Read<uint>(ref reader, "76-40-08-06", 0);        
        var data63 = Read<float>(ref reader, "C8-A7-9A-A5", 0);
        var data64 = Read<uint>(ref reader, "74-B6-7E-02", 0);

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        var data65 = Read<ScColor>(ref reader, "14-08-E9-15", 0);
        var data66 = Read<ScColor>(ref reader, "09-C9-C7-A2", 0);
    
        reader.Expect<uint>(5);

        var data67 = Read<uint>(ref reader, "BD-C8-8A-07", 0);
    
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);

        var data68 = Read<uint>(ref reader, "6A-B7-8A-A5", 0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);
        
        var data69 = Read<uint>(ref reader, "10-6D-19-75", 7);

        reader.Expect<uint>(0);

        var data70 = Read<float>(ref reader, "5A-C1-F6-4A", 0);
        var data71 = Read<float>(ref reader, "D9-0B-37-C3", 0);
        var data72 = Read<float>(ref reader, "A3-00-FA-B9", 0);
        var data73 = Read<float>(ref reader, "AF-F0-FB-62", 0);
        var data74 = Read<uint>(ref reader, "76-40-08-06", 0);
        var data75 = Read<float>(ref reader, "C8-A7-9A-A5", 0);
        var data76 = Read<uint>(ref reader, "74-B6-7E-02", 0);

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);
        
        var data77 = Read<ScColor>(ref reader, "14-08-E9-15", 0);
        var data78 = Read<ScColor>(ref reader, "09-C9-C7-A2", 0);

        reader.Expect<uint>(5);

        var data79 = Read<uint>(ref reader, "5E-88-47-A0", 0);
        
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);

        var data80 = Read<uint>(ref reader, "55-F0-9D-CE", 0);
        
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);
        
        var data81 = Read<uint>(ref reader, "4B-C4-36-97", 0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);
        
        var data82 = Read<ScColor>(ref reader, "AC-34-2A-44", 0);

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
        
        

        reader.ExpectBytes("A9-79-C9-73");
        var data84 = reader.Read<uint>();
        reader.Expect(0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        reader.ExpectBytes("97-07-53-BD");
        var data85 = reader.Read<ScColor>();
        reader.Expect(0);

        reader.Expect<uint>(5);

        reader.ExpectBytes("2C-A1-1F-A4");
        var data86 = reader.Read<uint>();
        reader.Expect(0);

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        reader.ExpectBytes("97-07-53-BD");
        var data87 = reader.Read<ScColor>();
        reader.Expect(0);
    }
}

public readonly record struct ScColor
{
    public readonly byte B;
    public readonly byte G;
    public readonly byte R;
    public readonly byte A;
}