using ChfUtils;

namespace ChfParser;

public sealed class FaceColors
{
    public required Color HeadColor { get; init; }
    public required Color EyeMakeupColor1 { get; init; }
    public required Color EyeMakeupColor2 { get; init; }
    public required Color EyeMakeupColor3 { get; init; }
    public required Color CheekMakeupColor1 { get; init; }
    public required Color CheekMakeupColor2 { get; init; }
    public required Color CheekMakeupColor3 { get; init; }
    public required Color LipMakeupColor1 { get; init; }
    public required Color LipMakeupColor2 { get; init; }
    public required Color LipMakeupColor3 { get; init; }
    public required uint Data10 { get; init; }
    public required uint Data11 { get; init; }
    public required uint Data12 { get; init; }
    public required uint Data13 { get; init; }
    public required uint Data14 { get; init; }
    public required uint Data15 { get; init; }
    public required uint Data16 { get; init; }
    public required uint Data17 { get; init; }
    public required uint Data18 { get; init; }
    public required uint Data19 { get; init; }
    public required uint Data20 { get; init; }
    public required uint Data21 { get; init; }
    
    public static FaceColors Read(ref SpanReader reader)
    {
        //note: the uints here are either a bitfield or a bool, not sure.
        reader.Expect<ulong>(0x16);
        
        var data22 = reader.ReadKeyValueAndChildCount<Color>(0, 0xbd530797);
        var data23 = reader.ReadKeyValueAndChildCount<Color>(0, 0xb29b1d90);
        var data24 = reader.ReadKeyValueAndChildCount<Color>(0, 0xe3230e2f);
        var data25 = reader.ReadKeyValueAndChildCount<Color>(0, 0x2ec0e736);
        var data26 = reader.ReadKeyValueAndChildCount<Color>(0, 0x1a081a93);
        var data27 = reader.ReadKeyValueAndChildCount<Color>(0, 0x4bb0092c);
        var data28 = reader.ReadKeyValueAndChildCount<Color>(0, 0x8653e035);
        var data29 = reader.ReadKeyValueAndChildCount<Color>(0, 0x7d86e792);
        var data30 = reader.ReadKeyValueAndChildCount<Color>(0, 0x2c3ef42d);
        var data31 = reader.ReadKeyValueAndChildCount<Color>(0, 0xe1dd1d34);
        var data32 = reader.ReadKeyValueAndChildCount<uint>(0, 0x64a583ec);
        var data33 = reader.ReadKeyValueAndChildCount<uint>(0, 0x77f57018);
        var data34 = reader.ReadKeyValueAndChildCount<uint>(0, 0xe9f3e598);
        var data35 = reader.ReadKeyValueAndChildCount<uint>(0, 0xfaa3166c);
        var data36 = reader.ReadKeyValueAndChildCount<uint>(0, 0x3cb379f2);
        var data37 = reader.ReadKeyValueAndChildCount<uint>(0, 0x2fe38a06);
        var data38 = reader.ReadKeyValueAndChildCount<uint>(0, 0x32b762f1);
        var data39 = reader.ReadKeyValueAndChildCount<uint>(0, 0x21e79105);
        var data40 = reader.ReadKeyValueAndChildCount<uint>(0, 0xf7e50257);
        var data41 = reader.ReadKeyValueAndChildCount<uint>(0, 0xe4b5f1a3);
        var data42 = reader.ReadKeyValueAndChildCount<uint>(0, 0x7b8b1fd6);
        var data43 = reader.ReadKeyValueAndChildCount<uint>(0, 0x68dbec22);
        
        return new FaceColors
        {
            HeadColor = data22,
            EyeMakeupColor1 = data23,
            EyeMakeupColor2 = data24,
            EyeMakeupColor3 = data25,
            CheekMakeupColor1 = data26,
            CheekMakeupColor2 = data27,
            CheekMakeupColor3 = data28,
            LipMakeupColor1 = data29,
            LipMakeupColor2 = data30,
            LipMakeupColor3 = data31,
            Data10 = data32,
            Data11 = data33,
            Data12 = data34,
            Data13 = data35,
            Data14 = data36,
            Data15 = data37,
            Data16 = data38,
            Data17 = data39,
            Data18 = data40,
            Data19 = data41,
            Data20 = data42,
            Data21 = data43
        };
    }
}