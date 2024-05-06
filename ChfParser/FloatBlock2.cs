using ChfUtils;

namespace ChfParser;

public sealed class FloatBlock2
{
    public required uint Key { get; init; }
    
    public required float Data01 { get; init; }
    public required float Data02 { get; init; }
    public required float Data03 { get; init; }
    public required float Data04 { get; init; }
    public required float Data05 { get; init; }
    public required float Data06 { get; init; }
    public required float Data07 { get; init; }
    
    public static FloatBlock2? Read(ref SpanReader reader)
    {
        //TODO: what is this?
        var k = reader.Read<uint>();
        reader.Expect(0);
        var count = reader.Read<ulong>();
        
        if (count == 0)
            return null;
        if (count != 7)
            throw new Exception($"Expected 7 floats, got {count}");

        var f01 = reader.ReadKeyValueAndChildCount<float>(0, 0x4a_f6_c1_5a);
        var f02 = reader.ReadKeyValueAndChildCount<float>(0, 0xc3_37_0b_d9);
        var f03 = reader.ReadKeyValueAndChildCount<float>(0, 0xb9_fa_00_a3);
        var f04 = reader.ReadKeyValueAndChildCount<float>(0, 0x62_fb_f0_af);
        var f05 = reader.ReadKeyValueAndChildCount<float>(0, 0x06_08_40_76);
        var f06 = reader.ReadKeyValueAndChildCount<float>(0, 0xa5_9a_a7_c8);
        var f07 = reader.ReadKeyValueAndChildCount<float>(0, 0x02_7e_b6_74);
        
        return new FloatBlock2
        {
            Key = k,
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