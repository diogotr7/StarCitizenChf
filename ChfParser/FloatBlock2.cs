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

        var f01 = reader.ReadKeyValueAndChildCount<float>(0, 0x4af6c15a);
        var f02 = reader.ReadKeyValueAndChildCount<float>(0, 0xc3370bd9);
        var f03 = reader.ReadKeyValueAndChildCount<float>(0, 0xb9fa00a3);
        var f04 = reader.ReadKeyValueAndChildCount<float>(0, 0x62fbf0af);
        var f05 = reader.ReadKeyValueAndChildCount<float>(0, 0x06084076);
        var f06 = reader.ReadKeyValueAndChildCount<float>(0, 0xa59aa7c8);
        var f07 = reader.ReadKeyValueAndChildCount<float>(0, 0x027eb674);

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