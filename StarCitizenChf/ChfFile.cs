
using System.Runtime.InteropServices;

namespace StarCitizenChf;

public sealed class ChfFile
{
    public const int Size = 4096;
    public const uint Magic = 0x00004242;
    
    public byte[] File { get; }

    public ChfFile(ReadOnlySpan<byte> uncompressedData)
    {
        File = new byte[Size];
        var span = File.AsSpan();
        var compressedData = Decompression.Compress(uncompressedData);
        
        MemoryMarshal.Write(span[0..4], Magic);
        MemoryMarshal.Write(span[4..8], 0);//placeholder crc32
        MemoryMarshal.Write(span[8..12], (uint)compressedData.Length);
        MemoryMarshal.Write(span[12..16], (uint)uncompressedData.Length);
        compressedData.CopyTo(span[16..]);
        Checksum.FixChecksum(File);
    }
}