
namespace ChfParser;

public sealed class FaceValues
{
    public required float FreckleAmount { get; init; }
    public required float FreckleOpacity { get; init; }
    public required float SunSpotsAmount { get; init; }
    public required float SunSpotOpacity { get; init; }
    public required float EyeMetallic1 { get; init; }
    public required float EyeMetallic2 { get; init; }
    public required float EyeMetallic3 { get; init; }
    public required float EyeSmoothness1 { get; init; }
    public required float EyeSmoothness2 { get; init; }
    public required float EyeSmoothness3 { get; init; }
    public required float EyeOpacity { get; init; }
    public required float CheekMetallic1 { get; init; }
    public required float CheekMetallic2 { get; init; }
    public required float CheekMetallic3 { get; init; }
    public required float CheekSmoothness1 { get; init; }
    public required float CheekSmoothness2 { get; init; }
    public required float CheekSmoothness3 { get; init; }
    public required float CheekOpacity { get; init; }
    public required float LipMetallic1 { get; init; }
    public required float LipMetallic2 { get; init; }
    public required float LipMetallic3 { get; init; }
    public required float LipSmoothness1 { get; init; }
    public required float LipSmoothness2 { get; init; }
    public required float LipSmoothness3 { get; init; }
    public required float LipOpacity { get; init; }
    
    public static FaceValues Read(ref SpanReader reader)
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
        
        return new FaceValues
        {
            FreckleAmount = freckleAmount,
            FreckleOpacity = freckleOpacity,
            SunSpotsAmount = sunSpotsAmount,
            SunSpotOpacity = sunSpotOpacity,
            EyeMetallic1 = data00,
            EyeMetallic2 = data01,
            EyeMetallic3 = data02,
            EyeSmoothness1 = data04,
            EyeSmoothness2 = data05,
            EyeSmoothness3 = data06,
            EyeOpacity = data07,
            CheekMetallic1 = data08,
            CheekMetallic2 = data09,
            CheekMetallic3 = data10,
            CheekSmoothness1 = data11,
            CheekSmoothness2 = data12,
            CheekSmoothness3 = data13,
            CheekOpacity = data14,
            LipMetallic1 = data15,
            LipMetallic2 = data16,
            LipMetallic3 = data17,
            LipSmoothness1 = data18,
            LipSmoothness2 = data19,
            LipSmoothness3 = data20,
            LipOpacity = data21,
        };
    }
}