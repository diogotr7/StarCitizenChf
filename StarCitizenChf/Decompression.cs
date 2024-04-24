using ZstdSharp;

namespace StarCitizenChf;

public static class Decompression
{
    public static async Task DecompressFile(string inputFilename, string outputFilename)
    {
        if (!inputFilename.EndsWith(".chf")) 
            throw new ArgumentException("Input file must be a .chf file", nameof(inputFilename));
        if (!outputFilename.EndsWith(".bin")) 
            throw new ArgumentException("Output file must be a .bin file", nameof(outputFilename));

        var compressed = await File.ReadAllBytesAsync(inputFilename);
        
        var decompressed = Decompress(compressed).ToArray();

        await File.WriteAllBytesAsync(outputFilename, decompressed);
    }
    
    public static async Task MutateFile(string inputFilename, string outputFilename, Action<byte[]> mutation)
    {
        var compressed = await File.ReadAllBytesAsync(inputFilename);
        
        var decompressed = Decompress(compressed).ToArray();
        
        mutation(decompressed);
        
        var reCompressed = Compress(decompressed).ToArray();
        
        reCompressed.CopyTo(compressed.AsSpan()[16..]);
        
        Checksum.FixChecksum(compressed);
        
        await File.WriteAllBytesAsync(outputFilename, compressed);
    }
    
    public static Span<byte> Decompress(ReadOnlySpan<byte> compressed)
    {
        if (compressed.Length < 4096)
            throw new ArgumentException("File must be 4096 bytes", nameof(compressed));
        if (compressed[0] != 0x42 || compressed[1] != 0x42 || compressed[2] != 0x00 || compressed[3] != 0x00)
            throw new ArgumentException("File must start with 0x42 0x42 0x00 0x00", nameof(compressed));
        
        var compressedSize = BitConverter.ToInt32(compressed[8..12]);
        var uncompressedSize = BitConverter.ToInt32(compressed[12..16]);
        
        using var zstd = new Decompressor();
        var uncompressed =  zstd.Unwrap(compressed.Slice(16, compressedSize), uncompressedSize);
        
        if (uncompressed.Length != uncompressedSize)
            throw new Exception("Decompressed size does not match expected size");
        
        return uncompressed;
    }
    
    public static Span<byte> Compress(ReadOnlySpan<byte> data)
    {
        using var zstd = new Compressor();
        return zstd.Wrap(data);
    }
}