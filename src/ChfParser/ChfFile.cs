using System.Numerics;
using System.Runtime.InteropServices;
using ZstdSharp;

namespace ChfParser;

public class ChfFile(byte[] data, bool isModded)
{
    public const int Size = 4096;
    private static ReadOnlySpan<byte> CigMagic => [0x42, 0x42, 0x00, 0x00];
    private static ReadOnlySpan<byte> MyMagic => "diogotr7"u8;
    
    public byte[] Data { get; } = data;
    public bool Modded { get; } = isModded;

    public static ChfFile FromBin(string file, bool isModded = true)
    {
        if (!file.EndsWith(".bin"))
            throw new ArgumentException("File must be a .bin file");
        
        var data = File.ReadAllBytes(file);
        
        return new ChfFile(data, isModded);
    }

    public static ChfFile FromChf(string file)
    {
        if (!file.EndsWith(".chf"))
            throw new ArgumentException("File must be a .chf file");
        
        var fileBytes = File.ReadAllBytes(file);
        var span = fileBytes.AsSpan();
        
        if (fileBytes.Length != Size)
            throw new ArgumentException("Invalid data length");
        
        if (!span.StartsWith(CigMagic))
            throw new ArgumentException("Invalid magic");
        
        var compressedSize = BitConverter.ToUInt32(span[8..12]);
        var uncompressedSize = BitConverter.ToUInt32(span[12..16]);
        
        var isModded = IsModded(span);
        
        var uncompressed = new byte[uncompressedSize];
        
        using var zstd = new Decompressor();
        var written = zstd.Unwrap(span[16..(int)(16 + compressedSize)], uncompressed);
        if (written != uncompressedSize)
            throw new Exception("Decompressed size does not match expected size");
        
        return new ChfFile(uncompressed, isModded);
    }
    
    public async Task WriteToChfFileAsync(string file)
    {
        if (!file.EndsWith(".chf"))
            throw new ArgumentException("File must be a .chf file");
        
        var compressed = GetChfBuffer();
        
        await File.WriteAllBytesAsync(file, compressed);
    }
    
    public async Task WriteToBinFileAsync(string file)
    {
        if (!file.EndsWith(".bin"))
            throw new ArgumentException("File must be a .bin file");
        
        await File.WriteAllBytesAsync(file, Data);
    }
    
    private byte[] GetChfBuffer()
    {
        using var zstd = new Compressor();
        
        var output = new byte[Size];
        var writtenBytes = zstd.Wrap(Data, output, 16);
        var span = output.AsSpan();
        
        CigMagic.CopyTo(span[..4]);
        MemoryMarshal.Write(span[4..8], 0);//placeholder crc32
        MemoryMarshal.Write(span[8..12], (uint)writtenBytes);
        MemoryMarshal.Write(span[12..16], (uint)Data.Length);
        
        //Insert our magic at the end so we can tell if it's a modded character.
        if (Modded)
            MyMagic.CopyTo(span[(Size - MyMagic.Length)..]);
        
        var acc = 0xFFFFFFFFu;
        foreach (ref readonly var t in span[16..])
        {
            acc = BitOperations.Crc32C(acc, t);
        }
        var crc = ~acc;
        
        BitConverter.TryWriteBytes(span[4..8], crc);
        
        return output;
    }
    
    private static bool IsModded(ReadOnlySpan<byte> data)
    {
        ReadOnlySpan<byte> zeroes = [0, 0, 0, 0, 0, 0, 0, 0];
        return data.EndsWith(MyMagic) || data.EndsWith(zeroes);
    }
}