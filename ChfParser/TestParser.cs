using ChfUtils;

namespace ChfParser;

public static class TestParser
{
    private static Guid ReadGuid(ref SpanReader reader, params string[] acceptableKeys)
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        reader.ExpectBytes(nextKey);
        return reader.Read<Guid>();
    }

    private static T Read<T>(ref SpanReader reader, int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        reader.ExpectBytes(nextKey);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }

    public static void Read(ref SpanReader reader)
    {
        FloatBlock.Read(ref reader);
        ColorBlock.Read(ref reader);
        reader.Expect<uint>(5);
        
        if (reader.NextKey == "93-4D-27-9B")
        {
            Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
            return;
        }
        
        if (reader.NextKey == "5E-88-47-A0")
        {
            Console.WriteLine($"Found {reader.NextKey}, remaining: {reader.Remaining.Length}");
            return;
        }

        //zero guids
        var data44 = ReadGuid(ref reader, "47-69-83-6C");
        var data45 = ReadGuid(ref reader,
            "FD-F9-81-B5",
            "42-FA-C9-1D",
            "50-1D-76-41",
            "FD-E1-3F-C7",
            "03-FC-42-38",
            "1A-97-72-D7",
            "1B-53-BF-35",
            "27-2A-83-F3",
            "27-32-3D-81",
            "2C-A6-0C-40",
            "2C-BE-B2-32",
            "2E-17-FF-56",
            "35-CD-3C-AF",
            "35-D5-82-DD",
            "38-32-85-93",
            "3E-41-B3-1C",
            "3E-59-0D-6E",
            "42-E2-77-6F",
            "49-76-46-AE",
            "4F-1D-CE-53",
            "56-76-FE-BC",
            "5B-89-47-80",
            "5B-91-F9-F2",
            "66-34-B6-A4",
            "7F-5F-86-4B",
            "81-42-FB-B4",
            "87-31-CD-3B",
            "8A-CE-74-07",
            "93-A5-44-E8",
            "98-29-CB-5B",
            "98-31-75-29",
            "9E-5A-FD-D4",
            "A5-94-3A-7F",
            "A9-5C-56-AC",
            "B7-6B-3B-51",
            "BC-FF-0A-90",
            "C0-5C-CE-E3",
            "CE-20-31-FD",
            "D9-37-FE-0C",
            "E4-8A-0F-28",
            "E4-92-B1-5A",
            "E9-75-B6-14",
            "EF-1E-3E-E9",
            "F0-1E-86-FB",
            "F6-75-0E-06",
            "4E-BF-1C-34"
        );

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        //-608, -240, -424
        if (reader.NextKey == "4F-9C-58-9B")
        {
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

        var data46 = Read<uint>(ref reader, 7,
            "02-0B-17-13",
            "08-0B-8F-BE",
            "08-0B-BB-9B",
            "0A-6B-87-28",
            "0F-59-78-5C",
            "12-AF-65-05",
            "17-AD-1F-1F",
            "1F-C5-58-BF",
            "1F-FD-0A-4A",
            "3C-D0-F6-B6",
            "62-2E-98-67",
            "C3-61-D0-AC",
            "C6-5F-70-FA",
            "D5-D1-1C-D5",
            "E6-79-62-2C",
            "FA-A1-6B-0E",
            "FA-E8-F2-CE"
        );

        reader.Expect(0);

        FloatBlock2.Read(ref reader);

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        ColorBlock2.Read(ref reader);
        
        reader.Expect<uint>(5);

        var data55 = ReadGuid(ref reader, "93-4D-27-9B", "BD-C8-8A-07");
        var data56 = ReadGuid(ref reader,
            "03-B7-7B-61",
            "04-A0-1F-2A",
            "04-EB-4F-F8",
            "05-C4-4D-EE",
            "0F-2C-90-99",
            "11-50-C4-3D",
            "16-47-A0-76",
            "1C-6A-0C-89",
            "1C-AF-7D-01",
            "1D-80-7F-17",
            "20-6E-60-93",
            "26-1D-56-1C",
            "27-79-04-D8",
            "39-05-50-7C",
            "3E-12-34-37",
            "3F-76-66-F3",
            "42-A9-4E-36",
            "49-25-C1-85",
            "4E-32-A5-CE",
            "50-4E-F1-6A",
            "57-59-95-21",
            "61-70-55-C4",
            "67-03-63-4B",
            "6A-FC-DA-77",
            "6B-5D-F9-3B",
            "6D-EB-BE-3C",
            "72-36-C9-D4",
            "73-97-EA-98",
            "78-1B-65-2B",
            "7E-68-53-A4",
            "7F-0C-01-60",
            "85-B4-8C-63",
            "8C-F6-7B-D1",
            "8D-92-29-15",
            "93-EE-7D-B1",
            "94-F9-19-FA",
            "A2-D0-D9-1F",
            "A9-17-06-7E",
            "A9-5C-56-AC",
            "AE-4B-32-E7",
            "AF-2F-60-23",
            "B0-7C-36-91",
            "B1-96-45-0F",
            "B6-44-50-CC",
            "B7-20-02-08",
            "BB-BB-E9-F0",
            "C6-A1-B0-BD",
            "C7-00-93-F1",
            "C7-4B-C3-23",
            "CB-9B-78-09",
            "CD-E8-4E-86",
            "D2-F0-48-E6",
            "D4-83-7E-69",
            "D5-E7-2C-AD",
            "D9-7C-C7-55",
            "DE-6B-A3-1E",
            "DF-0F-7F-46",
            "DF-CA-80-52",
            "E0-8C-5E-53",
            "E3-CE-EC-48",
            "E8-42-63-FB",
            "FA-A5-DC-A7"
        );

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        var data57 = Read<uint>(ref reader, 7,
            "01-1A-1E-41",
            "04-4E-91-B2",
            "0A-C9-C7-EB",
            "10-6D-19-75",
            "18-5A-60-87",
            "1A-E5-9E-F8",
            "1B-D9-0B-75",
            "1D-3F-76-3A",
            "20-72-82-EC",
            "23-F1-E9-1E",
            "54-69-4B-0D",
            "57-EA-20-FF",
            "63-AD-37-9F",
            "69-15-26-95",
            "6A-96-4D-67",
            "6C-41-A9-66",
            "6F-C2-C2-94",
            "70-55-58-53",
            "74-5B-86-4E",
            "75-01-D7-A0",
            "79-09-E9-01",
            "81-F2-87-B3",
            "82-71-EC-41",
            "84-A6-08-40",
            "87-25-63-B2",
            "9B-31-92-87",
            "9D-E6-76-86",
            "9E-65-1D-74",
            "B9-DA-65-D8",
            "BA-59-0E-2A",
            "EC-A9-30-94",
            "EF-2A-5B-66",
            "F0-BD-C1-A1",
            "F3-3E-AA-53",
            "73-D6-33-A1"
        );
        reader.Expect<uint>(0);

        FloatBlock2.Read(ref reader);

        reader.Expect<uint>(2);
        reader.Expect<uint>(0);

        ColorBlock2.Read(ref reader);

        reader.Expect<uint>(5);

        var data67 = ReadGuid(ref reader, "BD-C8-8A-07", "5E-88-47-A0");

        var data68 = ReadGuid(ref reader,
            "1C-6A-0C-89",
            "04-EB-4F-F8",
            "1D-80-7F-17",
            "55-F0-9D-CE",
            "6A-B7-8A-A5",
            "6B-5D-F9-3B",
            "72-36-C9-D4",
            "A9-17-06-7E",
            "B0-7C-36-91",
            "B1-96-45-0F",
            "C6-A1-B0-BD",
            "C7-4B-C3-23",
            "DF-CA-80-52"
        );

        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        if (reader.NextKey != "4B-C4-36-97")
        {
            var data69 = Read<uint>(ref reader, 7,
                "0A-C9-C7-EB",
                "10-6D-19-75",
                "1D-3F-76-3A",
                "63-AD-37-9F",
                "74-5B-86-4E",
                "79-09-E9-01"
            );

            //336 remaining
            reader.Expect<uint>(0);
            
            FloatBlock2.Read(ref reader);

            reader.Expect<uint>(2);
            reader.Expect<uint>(0);

            ColorBlock2.Read(ref reader);

            reader.Expect<uint>(5);

            var data79 = ReadGuid(ref reader, "5E-88-47-A0");
            var data80 = ReadGuid(ref reader, "55-F0-9D-CE");

            reader.Expect<uint>(1);
            reader.Expect<uint>(5);
        }

        var data81 = Read<uint>(ref reader, 0, "4B-C4-36-97");

        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);

        var data82 = Read<Color>(ref reader, 0, "AC-34-2A-44");

        reader.Expect<uint>(5);

        reader.ExpectBytes("58-4D-42-27");
        var someguid = reader.Read<Guid>();
        var additioonal = reader.Read<uint>();

        //100 remaining
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

public sealed class FloatBlock
{
    private static T Read<T>(ref SpanReader reader, int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        reader.ExpectBytes(nextKey);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
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
    }
}

public sealed class FloatBlock2
{
    private static T Read<T>(ref SpanReader reader, int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        reader.ExpectBytes(nextKey);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }
    
    public static void Read(ref SpanReader reader)
    {
        var f01 = Read<float>(ref reader, 0, "5A-C1-F6-4A");
        var f02 = Read<float>(ref reader, 0, "D9-0B-37-C3");
        var f03 = Read<float>(ref reader, 0, "A3-00-FA-B9");
        var f04 = Read<float>(ref reader, 0, "AF-F0-FB-62");
        var f05 = Read<float>(ref reader, 0, "76-40-08-06");
        var f06 = Read<float>(ref reader, 0, "C8-A7-9A-A5");
        var f07 = Read<float>(ref reader, 0, "74-B6-7E-02");
    }
}

public sealed class ColorBlock
{
    private static T Read<T>(ref SpanReader reader, int count, string key) where T : unmanaged
    {
        reader.ExpectBytes(key);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }
    
    public static void Read(ref SpanReader reader)
    {
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
    }
}

public sealed class ColorBlock2
{
    private static T Read<T>(ref SpanReader reader, int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = reader.NextKey;
        if (!acceptableKeys.Contains(reader.NextKey))
            throw new Exception($"Unexpected key: {nextKey}");

        reader.ExpectBytes(nextKey);
        var data = reader.Read<T>();
        reader.Expect(count);
        return data;
    }
    
    public static void Read(ref SpanReader reader)
    {
        var data53 = Read<Color>(ref reader, 0, "14-08-E9-15");
        var data54 = Read<Color>(ref reader, 0, "09-C9-C7-A2");
    }
}