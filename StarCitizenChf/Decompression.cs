using ZstdSharp;

namespace StarCitizenChf;

public static class Decompression
{
    public static async Task Decompress(string inputFilename, string outputFilename)
    {
        if (!inputFilename.EndsWith(".chf")) 
            throw new ArgumentException("Input file must be a .chf file", nameof(inputFilename));
        if (!outputFilename.EndsWith(".bin")) 
            throw new ArgumentException("Output file must be a .bin file", nameof(outputFilename));
        
        using var zstd = new Decompressor();
        
        var compressed = await File.ReadAllBytesAsync(inputFilename);
        var lastDataByte = FindLastDataByte(compressed);

        var decompressed = zstd.Unwrap(compressed.AsSpan()[16..lastDataByte]).ToArray();

        if (decompressed.Length == 0)
            return;

        await File.WriteAllBytesAsync(outputFilename, decompressed);
    }
    
    private static int FindLastDataByte(ReadOnlySpan<byte> data)
    {
        if (data.Length != 4096)
            throw new ArgumentException("Span must be 4096 bytes long", nameof(data));
        
        //find first 7 zero bytes in a row
        return data.IndexOf<byte>([0, 0, 0, 0, 0, 0, 0]);
    }
}