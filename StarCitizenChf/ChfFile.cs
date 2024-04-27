using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace StarCitizenChf;

public sealed class ChfFile
{
    public const int Size = 4096;
    public const uint CigMagic = 0x00004242;
    private static ReadOnlySpan<byte> MyMagic => "diogotr7"u8;

    private readonly byte[] _data;
        
    private ChfFile(byte[] data)
    {
        _data = data;
    }
    
    public static ChfFile FromUncompressed(ReadOnlySpan<byte> uncompressedData)
    {
        var data = new byte[Size];
        var span = data.AsSpan();
        var compressedData = Decompression.Compress(uncompressedData);
        
        MemoryMarshal.Write(span[0..4], CigMagic);
        MemoryMarshal.Write(span[4..8], 0);//placeholder crc32
        MemoryMarshal.Write(span[8..12], (uint)compressedData.Length);
        MemoryMarshal.Write(span[12..16], (uint)uncompressedData.Length);
        compressedData.CopyTo(span[16..]);

        //Insert our magic at the end so we can tell if it's a modded character.
        var magic = MyMagic;
        magic.CopyTo(span[(Size - magic.Length)..]);
        
        var acc = 0xFFFFFFFFu;
        foreach (ref readonly var t in span[16..])
        {
            acc = BitOperations.Crc32C(acc, t);
        }
        var crc =  ~acc;
        
        BitConverter.TryWriteBytes(span[4..8], crc);
        
        return new ChfFile(data);
    }
    
    public void WriteToFile(string file)
    {
        if (!file.EndsWith(".chf"))
            throw new ArgumentException("File must be a .chf file");
        
        File.WriteAllBytes(file, _data);
    }
    
    public async Task WriteToFileAsync(string file)
    {
        if (!file.EndsWith(".chf"))
            throw new ArgumentException("File must be a .chf file");
        
        await File.WriteAllBytesAsync(file, _data);
    }
    
    public static ChfFile FromBin(string file)
    {
        if (!file.EndsWith(".bin"))
            throw new ArgumentException("File must be a .bin file");
        
        var data = File.ReadAllBytes(file);
        
        return FromUncompressed(data);
    }
    
    public static ChfFile FromChf(string file)
    {
        if (!file.EndsWith(".chf"))
            throw new ArgumentException("File must be a .chf file");
        
        var data = File.ReadAllBytes(file);
        
        if (data.Length != Size)
            throw new ArgumentException("Invalid data length");
        
        if (BitConverter.ToUInt32(data.AsSpan()[..4]) != CigMagic)
            throw new ArgumentException("Invalid magic");
        
        return new ChfFile(data);
    }
    
    public bool IsModded()
    {
        ReadOnlySpan<byte> zeroes = [0, 0, 0, 0, 0, 0, 0, 0];
        var span = _data.AsSpan();
        return span.EndsWith(MyMagic) || span.EndsWith(zeroes);
    }
}