using ChfUtils;

namespace ChfParser;

public sealed class ColorBlock
{
    public required Color Data00 { get; init; }
    public required Color Data01 { get; init; }
    public required Color Data02 { get; init; }
    public required Color Data03 { get; init; }
    public required Color Data04 { get; init; }
    public required Color Data05 { get; init; }
    public required Color Data06 { get; init; }
    public required Color Data07 { get; init; }
    public required Color Data08 { get; init; }
    public required Color Data09 { get; init; }
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
    
    
    public static ColorBlock Read(ref SpanReader reader)
    {
        reader.Expect<ulong>(0x16);
        var data22 = reader.ReadKeyValueAndChildCount<Color>(0, "97-07-53-BD");
        var data23 = reader.ReadKeyValueAndChildCount<Color>(0, "90-1D-9B-B2");
        var data24 = reader.ReadKeyValueAndChildCount<Color>(0, "2F-0E-23-E3");
        var data25 = reader.ReadKeyValueAndChildCount<Color>(0, "36-E7-C0-2E");
        var data26 = reader.ReadKeyValueAndChildCount<Color>(0, "93-1A-08-1A");
        var data27 = reader.ReadKeyValueAndChildCount<Color>(0, "2C-09-B0-4B");
        var data28 = reader.ReadKeyValueAndChildCount<Color>(0, "35-E0-53-86");
        var data29 = reader.ReadKeyValueAndChildCount<Color>(0, "92-E7-86-7D");
        var data30 = reader.ReadKeyValueAndChildCount<Color>(0, "2D-F4-3E-2C");
        var data31 = reader.ReadKeyValueAndChildCount<Color>(0, "34-1D-DD-E1");
        var data32 = reader.ReadKeyValueAndChildCount<uint>(0, "EC-83-A5-64");
        var data33 = reader.ReadKeyValueAndChildCount<uint>(0, "18-70-F5-77");
        var data34 = reader.ReadKeyValueAndChildCount<uint>(0, "98-E5-F3-E9");
        var data35 = reader.ReadKeyValueAndChildCount<uint>(0, "6C-16-A3-FA");
        var data36 = reader.ReadKeyValueAndChildCount<uint>(0, "F2-79-B3-3C");
        var data37 = reader.ReadKeyValueAndChildCount<uint>(0, "06-8A-E3-2F");
        var data38 = reader.ReadKeyValueAndChildCount<uint>(0, "F1-62-B7-32");
        var data39 = reader.ReadKeyValueAndChildCount<uint>(0, "05-91-E7-21");
        var data40 = reader.ReadKeyValueAndChildCount<uint>(0, "57-02-E5-F7");
        var data41 = reader.ReadKeyValueAndChildCount<uint>(0, "A3-F1-B5-E4");
        var data42 = reader.ReadKeyValueAndChildCount<uint>(0, "D6-1F-8B-7B");
        var data43 = reader.ReadKeyValueAndChildCount<uint>(0, "22-EC-DB-68");
        
        return new ColorBlock()
        {
            Data00 = data22,
            Data01 = data23,
            Data02 = data24,
            Data03 = data25,
            Data04 = data26,
            Data05 = data27,
            Data06 = data28,
            Data07 = data29,
            Data08 = data30,
            Data09 = data31,
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