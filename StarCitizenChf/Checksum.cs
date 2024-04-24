using System.Numerics;

namespace StarCitizenChf;

public static class Checksum
{
    public static uint Crc32c(ReadOnlySpan<byte> data)
    {
        if (data.Length != 4096 - 16)
            throw new ArgumentException("Invalid data length");
        
        var crc = 0xFFFFFFFFu;
        
        foreach (ref readonly var t in data)
        {
            crc = BitOperations.Crc32C(crc, t);
        }
        
        return ~crc;
    }
    
    public static void FixChecksum(Span<byte> data)
    {
        var crc = Crc32c(data[16..]);
        BitConverter.TryWriteBytes(data[4..8], crc);
    }
}