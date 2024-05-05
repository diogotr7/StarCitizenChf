using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class FloatBlock2
{
    public required float Data01 { get; init; }
    public required float Data02 { get; init; }
    public required float Data03 { get; init; }
    public required float Data04 { get; init; }
    public required float Data05 { get; init; }
    public required float Data06 { get; init; }
    public required float Data07 { get; init; }
    
    public static FloatBlock2? Read(ref SpanReader reader)
    {
        var k = reader.Read<uint>();
        Console.WriteLine($"FloatBlock2: 0x{k:X8}");
        reader.Expect(0);
        var count = reader.Read<ulong>();
        
        if (count == 0)
            return null;
        if (count != 7)
            throw new Exception($"Expected 7 floats, got {count}");

        var f01 = reader.ReadKeyValueAndChildCount<float>(0, "5A-C1-F6-4A");
        var f02 = reader.ReadKeyValueAndChildCount<float>(0, "D9-0B-37-C3");
        var f03 = reader.ReadKeyValueAndChildCount<float>(0, "A3-00-FA-B9");
        var f04 = reader.ReadKeyValueAndChildCount<float>(0, "AF-F0-FB-62");
        var f05 = reader.ReadKeyValueAndChildCount<float>(0, "76-40-08-06");
        var f06 = reader.ReadKeyValueAndChildCount<float>(0, "C8-A7-9A-A5");
        var f07 = reader.ReadKeyValueAndChildCount<float>(0, "74-B6-7E-02");
        
        return new FloatBlock2()
        {
            Data01 = f01,
            Data02 = f02,
            Data03 = f03,
            Data04 = f04,
            Data05 = f05,
            Data06 = f06,
            Data07 = f07
        };
    }
}