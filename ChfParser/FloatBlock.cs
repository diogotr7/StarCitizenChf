using ChfUtils;

namespace ChfParser;

public sealed class FloatBlock
{
    public required float FreckleAmount { get; init; }
    public required float FreckleOpacity { get; init; }
    public required float SunSpotsAmount { get; init; }
    public required float SunSpotOpacity { get; init; }
    public required float Data00 { get; init; }
    public required float Data01 { get; init; }
    public required float Data02 { get; init; }
    public required float Data04 { get; init; }
    public required float Data05 { get; init; }
    public required float Data06 { get; init; }
    public required float Data07 { get; init; }
    public required float Data08 { get; init; }
    public required float Data09 { get; init; }
    public required float Data10 { get; init; }
    public required float Data11 { get; init; }
    public required float Data12 { get; init; }
    public required float Data13 { get; init; }
    public required float Data14 { get; init; }
    public required float Data15 { get; init; }
    public required float Data16 { get; init; }
    public required float Data17 { get; init; }
    public required float Data18 { get; init; }
    public required float Data19 { get; init; }
    public required float Data20 { get; init; }
    public required float Data21 { get; init; }
    
    public static FloatBlock Read(ref SpanReader reader)
    {
        reader.Expect<ulong>(25);
        var freckleAmount = reader.Read<float>(0, "E2-27-77-E8");
        var freckleOpacity = reader.Read<float>(0, "58-CB-61-93");
        var sunSpotsAmount = reader.Read<float>(0, "0F-D2-4A-55");
        var sunSpotOpacity = reader.Read<float>(0, "64-12-C4-CF");
        var data00 = reader.Read<float>(0, "B0-83-58-B9");
        var data01 = reader.Read<float>(0, "C3-50-F7-9C");
        var data02 = reader.Read<float>(0, "DF-44-06-A9");
        var data04 = reader.Read<float>(0, "87-A9-71-C8");
        var data05 = reader.Read<float>(0, "F4-7A-DE-ED");
        var data06 = reader.Read<float>(0, "E8-6E-2F-D8");
        var data07 = reader.Read<float>(0, "BA-26-E5-CA");
        var data08 = reader.Read<float>(0, "02-ED-26-05");
        var data09 = reader.Read<float>(0, "71-3E-89-20");
        var data10 = reader.Read<float>(0, "6D-2A-78-15");
        var data11 = reader.Read<float>(0, "D7-D5-E3-9B");
        var data12 = reader.Read<float>(0, "A4-06-4C-BE");
        var data13 = reader.Read<float>(0, "B8-12-BD-8B");
        var data14 = reader.Read<float>(0, "D3-A1-A1-11");
        var data15 = reader.Read<float>(0, "C3-1A-57-92");
        var data16 = reader.Read<float>(0, "B0-C9-F8-B7");
        var data17 = reader.Read<float>(0, "AC-DD-09-82");
        var data18 = reader.Read<float>(0, "E7-01-92-AA");
        var data19 = reader.Read<float>(0, "94-D2-3D-8F");
        var data20 = reader.Read<float>(0, "88-C6-CC-BA");
        var data21 = reader.Read<float>(0, "F4-DC-9D-58");
        
        return new FloatBlock()
        {
            FreckleAmount = freckleAmount,
            FreckleOpacity = freckleOpacity,
            SunSpotsAmount = sunSpotsAmount,
            SunSpotOpacity = sunSpotOpacity,
            Data00 = data00,
            Data01 = data01,
            Data02 = data02,
            Data04 = data04,
            Data05 = data05,
            Data06 = data06,
            Data07 = data07,
            Data08 = data08,
            Data09 = data09,
            Data10 = data10,
            Data11 = data11,
            Data12 = data12,
            Data13 = data13,
            Data14 = data14,
            Data15 = data15,
            Data16 = data16,
            Data17 = data17,
            Data18 = data18,
            Data19 = data19,
            Data20 = data20,
            Data21 = data21,
        };
    }
}