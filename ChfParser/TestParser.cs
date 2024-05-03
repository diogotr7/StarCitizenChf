using ChfUtils;

namespace ChfParser;

//Below this is whatever appears after bodymaterial in the smallest files.
//the data is different, but the key-data-childcount pattern is common.
//sometimes the data is a color, integer or float, in which case the block is 12 bytes.
//some other times, the data is a guid, in which case the block is 16 + 4 + 4 = 24 bytes.
public static class TestParser
{
    private static T ReadInternal<T>(ref SpanReader reader, int count, string key) where T : unmanaged
    {
        reader.ExpectBytes(key);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }

    private static Guid ReadGuidInternal(ref SpanReader reader, string key)
    {
        reader.ExpectBytes(key);
        return reader.Read<Guid>();
    }

    private static Guid ReadGuid(ref SpanReader reader, params string[] acceptableKeys)
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        return ReadGuidInternal(ref reader, reader.NextKey);
    }
    
    private static T Read<T>(ref SpanReader reader, int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        return ReadInternal<T>(ref reader, count, reader.NextKey);
    }

    public static void Read(ref SpanReader reader)
    {
        reader.Expect<ulong>(25);
        var freckleAmount = Read<float>(ref reader, 0, "E2-27-77-E8");
        var freckleOpacity = Read<float>(ref reader, 0, "58-CB-61-93");
        var sunSpotsAmount = Read<float>(ref reader, 0, "0F-D2-4A-55");
        var sunSpotOpacity = Read<float>(ref reader, 0, "64-12-C4-CF");
        var data00 = Read<float>(ref reader, 0, "B0-83-58-B9");
        var data01 = Read<float>(ref reader, 0, "C3-50-F7-9C");
        var data02 = Read<float>(ref reader, 0, "DF-44-06-A9");
        var data04 = Read<float>(ref reader, 0, "87-A9-71-C8");
        var data05 = Read<float>(ref reader, 0, "F4-7A-DE-ED");
        var data06 = Read<float>(ref reader, 0, "E8-6E-2F-D8");
        var data07 = Read<float>(ref reader, 0, "BA-26-E5-CA");
        var data08 = Read<float>(ref reader, 0, "02-ED-26-05");
        var data09 = Read<float>(ref reader, 0, "71-3E-89-20");
        var data10 = Read<float>(ref reader, 0, "6D-2A-78-15");
        var data11 = Read<float>(ref reader, 0, "D7-D5-E3-9B");
        var data12 = Read<float>(ref reader, 0, "A4-06-4C-BE");
        var data13 = Read<float>(ref reader, 0, "B8-12-BD-8B");
        var data14 = Read<float>(ref reader, 0, "D3-A1-A1-11");
        var data15 = Read<float>(ref reader, 0, "C3-1A-57-92");
        var data16 = Read<float>(ref reader, 0, "B0-C9-F8-B7");
        var data17 = Read<float>(ref reader, 0, "AC-DD-09-82");
        var data18 = Read<float>(ref reader, 0, "E7-01-92-AA");
        var data19 = Read<float>(ref reader, 0, "94-D2-3D-8F");
        var data20 = Read<float>(ref reader, 0, "88-C6-CC-BA");
        var data21 = Read<float>(ref reader, 0, "F4-DC-9D-58");

        reader.Expect<ulong>(22);
        var data22 = Read<Color>(ref reader, 0, "97-07-53-BD");
        var data23 = Read<Color>(ref reader, 0, "90-1D-9B-B2");
        var data24 = Read<Color>(ref reader, 0, "2F-0E-23-E3");
        var data25 = Read<Color>(ref reader, 0, "36-E7-C0-2E");
        var data26 = Read<Color>(ref reader, 0, "93-1A-08-1A");
        var data27 = Read<Color>(ref reader, 0, "2C-09-B0-4B");
        var data28 = Read<Color>(ref reader, 0, "35-E0-53-86");
        var data29 = Read<Color>(ref reader, 0, "92-E7-86-7D");
        var data30 = Read<Color>(ref reader, 0, "2D-F4-3E-2C");
        var data31 = Read<Color>(ref reader, 0, "34-1D-DD-E1");
        var data32 = Read<uint>(ref reader, 0, "EC-83-A5-64");
        var data33 = Read<uint>(ref reader, 0, "18-70-F5-77");
        var data34 = Read<uint>(ref reader, 0, "98-E5-F3-E9");
        var data35 = Read<uint>(ref reader, 0, "6C-16-A3-FA");
        var data36 = Read<uint>(ref reader, 0, "F2-79-B3-3C");
        var data37 = Read<uint>(ref reader, 0, "06-8A-E3-2F");
        var data38 = Read<uint>(ref reader, 0, "F1-62-B7-32");
        var data39 = Read<uint>(ref reader, 0, "05-91-E7-21");
        var data40 = Read<uint>(ref reader, 0, "57-02-E5-F7");
        var data41 = Read<uint>(ref reader, 0, "A3-F1-B5-E4");
        var data42 = Read<uint>(ref reader, 0, "D6-1F-8B-7B");
        var data43 = Read<uint>(ref reader, 0, "22-EC-DB-68");

        reader.Expect<uint>(5);

        //zero guids
        var data44 = ReadGuid(ref reader, "47-69-83-6C");
        var data45 = ReadGuid(ref reader, 
            "8A-CE-74-07",
            "42-E2-77-6F",
            "C0-5C-CE-E3",
            "9E-5A-FD-D4",
            "E9-75-B6-14",
            "03-FC-42-38",
            "5B-91-F9-F2"
            );

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data46 = Read<uint>(ref reader, 7, "17-AD-1F-1F", "D5-D1-1C-D5", "0F-59-78-5C", "02-0B-17-13");

        reader.Expect(0);

        var data47_ = Read<float>(ref reader, 0, "5A-C1-F6-4A");
        var data47 = Read<float>(ref reader, 0, "D9-0B-37-C3");
        var data48 = Read<float>(ref reader, 0, "A3-00-FA-B9");
        var data49 = Read<float>(ref reader, 0, "AF-F0-FB-62");
        var data50 = Read<float>(ref reader, 0, "76-40-08-06");
        var data51 = Read<float>(ref reader, 0, "C8-A7-9A-A5");
        var data52 = Read<float>(ref reader, 0, "74-B6-7E-02");

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        var data53 = Read<Color>(ref reader, 0, "14-08-E9-15");
        var data54 = Read<Color>(ref reader, 0, "09-C9-C7-A2");
        reader.Expect<uint>(5);

        var data55 = ReadGuid(ref reader, "93-4D-27-9B", "BD-C8-8A-07");
        var data56 = ReadGuid(ref reader, 
            "A9-5C-56-AC",
            "A2-D0-D9-1F",
            "8D-92-29-15",
            "39-05-50-7C",
            "8C-F6-7B-D1",
            "E3-CE-EC-48",
            "6B-5D-F9-3B"
            );

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data57 = Read<uint>(ref reader, 7,  
            "9B-31-92-87",
            "1B-D9-0B-75",
            "9D-E6-76-86",
            "75-01-D7-A0",
            "BA-59-0E-2A",
            "81-F2-87-B3",
            "63-AD-37-9F"
        );
        reader.Expect<uint>(0);

        var data58 = Read<float>(ref reader, 0, "5A-C1-F6-4A");
        var data59 = Read<float>(ref reader, 0, "D9-0B-37-C3");
        var data60 = Read<float>(ref reader, 0, "A3-00-FA-B9");
        var data61 = Read<float>(ref reader, 0, "AF-F0-FB-62");
        var data62 = Read<float>(ref reader, 0, "76-40-08-06");
        var data63 = Read<float>(ref reader, 0, "C8-A7-9A-A5");
        var data64 = Read<float>(ref reader, 0, "74-B6-7E-02");

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        var data65 = Read<Color>(ref reader, 0, "14-08-E9-15");
        var data66 = Read<Color>(ref reader, 0, "09-C9-C7-A2");

        reader.Expect<uint>(5);

        var data67 = ReadGuid(ref reader, "BD-C8-8A-07", "5E-88-47-A0");
        var data68 = ReadGuid(ref reader, 
            "6A-B7-8A-A5",
            "B0-7C-36-91",
            "04-EB-4F-F8",
            "1D-80-7F-17",
            "6B-5D-F9-3B",
            "55-F0-9D-CE");

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data69 = Read<uint>(ref reader, 7, 
            "10-6D-19-75",
            "63-AD-37-9F",
            "74-5B-86-4E",
            "0A-C9-C7-EB",
            "4B-C4-36-97");//if 4c-c4 etc then count is zero.

        reader.Expect<uint>(0);

        var data70 = Read<float>(ref reader, 0, "5A-C1-F6-4A");
        var data71 = Read<float>(ref reader, 0, "D9-0B-37-C3");
        var data72 = Read<float>(ref reader, 0, "A3-00-FA-B9");
        var data73 = Read<float>(ref reader, 0, "AF-F0-FB-62");
        var data74 = Read<float>(ref reader, 0, "76-40-08-06");
        var data75 = Read<float>(ref reader, 0, "C8-A7-9A-A5");
        var data76 = Read<float>(ref reader, 0, "74-B6-7E-02");

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        var data77 = Read<Color>(ref reader, 0, "14-08-E9-15");
        var data78 = Read<Color>(ref reader, 0, "09-C9-C7-A2");

        reader.Expect<uint>(5);

        var data79 = ReadGuid(ref reader, "5E-88-47-A0");
        var data80 = ReadGuid(ref reader, "55-F0-9D-CE");

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data81 = Read<uint>(ref reader, 0, "4B-C4-36-97");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data82 = Read<Color>(ref reader, 0, "AC-34-2A-44");

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

        var data83 = Read<uint>(ref reader, 0, "A9-79-C9-73", "4C-6E-6B-31");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data84 = Read<Color>(ref reader, 0, "97-07-53-BD");

        reader.Expect<uint>(5);

        var data85 = Read<uint>(ref reader, 0, "2C-A1-1F-A4", "DB-66-5B-8A");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data86 = Read<Color>(ref reader, 0, "97-07-53-BD");
    }
}