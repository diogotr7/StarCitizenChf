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
        reader.Expect<ulong>(0x19);
        
        var freckleAmount = reader.ReadKeyValueAndChildCount<float>(0, 0xe87727e2);
        var freckleOpacity = reader.ReadKeyValueAndChildCount<float>(0, 0x9361cb58);
        var sunSpotsAmount = reader.ReadKeyValueAndChildCount<float>(0, 0x554ad20f);
        var sunSpotOpacity = reader.ReadKeyValueAndChildCount<float>(0, 0xcfc41264);
        var data00 = reader.ReadKeyValueAndChildCount<float>(0, 0xb95883b0);
        var data01 = reader.ReadKeyValueAndChildCount<float>(0, 0x9cf750c3);
        var data02 = reader.ReadKeyValueAndChildCount<float>(0, 0xa90644df);
        var data04 = reader.ReadKeyValueAndChildCount<float>(0, 0xc871a987);
        var data05 = reader.ReadKeyValueAndChildCount<float>(0, 0xedde7af4);
        var data06 = reader.ReadKeyValueAndChildCount<float>(0, 0xd82f6ee8);
        var data07 = reader.ReadKeyValueAndChildCount<float>(0, 0xcae526ba);
        var data08 = reader.ReadKeyValueAndChildCount<float>(0, 0x0526ed02);
        var data09 = reader.ReadKeyValueAndChildCount<float>(0, 0x20893e71);
        var data10 = reader.ReadKeyValueAndChildCount<float>(0, 0x15782a6d);
        var data11 = reader.ReadKeyValueAndChildCount<float>(0, 0x9be3d5d7);
        var data12 = reader.ReadKeyValueAndChildCount<float>(0, 0xbe4c06a4);
        var data13 = reader.ReadKeyValueAndChildCount<float>(0, 0x8bbd12b8);
        var data14 = reader.ReadKeyValueAndChildCount<float>(0, 0x11a1a1d3);
        var data15 = reader.ReadKeyValueAndChildCount<float>(0, 0x92571ac3);
        var data16 = reader.ReadKeyValueAndChildCount<float>(0, 0xb7f8c9b0);
        var data17 = reader.ReadKeyValueAndChildCount<float>(0, 0x8209ddac);
        var data18 = reader.ReadKeyValueAndChildCount<float>(0, 0xaa9201e7);
        var data19 = reader.ReadKeyValueAndChildCount<float>(0, 0x8f3dd294);
        var data20 = reader.ReadKeyValueAndChildCount<float>(0, 0xbaccc688);
        var data21 = reader.ReadKeyValueAndChildCount<float>(0, 0x589ddcf4);
        
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