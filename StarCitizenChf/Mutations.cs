namespace StarCitizenChf;

public static class Mutations
{
    public static void ChangeBodyType(Span<byte> data)
    {
        ReadOnlySpan<byte> woman = [0xAD, 0x4C, 0xB0, 0xEF, 0x94, 0x4A, 0x79, 0xD0, 0x53, 0xC2, 0xD3, 0xB4, 0x58, 0x25, 0x38, 0xAD];
        ReadOnlySpan<byte> man = [0x61, 0x4A, 0x6B, 0x14, 0xD5, 0x39, 0xF4, 0x25, 0x49, 0x8A, 0xB6, 0xDF, 0x86, 0xA4, 0x99, 0xA9];

        var indexOfWoman = data.IndexOf(woman);
        if (indexOfWoman != -1)
        {
            woman.CopyTo(data.Slice(indexOfWoman, 16));
            return;
        }

        var indexOfMan = data.IndexOf(man);
        if (indexOfMan != -1)
        {
            man.CopyTo(data.Slice(indexOfMan, 16));
            return;
        }
    }
}