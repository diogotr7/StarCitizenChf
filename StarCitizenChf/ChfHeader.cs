using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StarCitizenChf;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ChfHeader
{
    public const int Size = 16;
    public const uint MagicBytes = 0x4242;
    public uint Magic;
    public uint Crc32c;
    public uint CompressedSize;
    public uint UncompressedSize;
}

/// <summary>
///     I have no idea what the contents of this are. It is 8 bytes long.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ChfFooter
{
    public const int Size = 8;
    public byte Unknown1;
    public byte Unknown2;
    public byte Unknown3;
    public byte Unknown4;
    public byte Unknown5;
    public byte Unknown6;
    public byte Unknown7;
    public byte Unknown8;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[InlineArray(ChfFile.Size - ChfHeader.Size - ChfFooter.Size)]
public struct ChfData
{
    private byte _data;
}

public struct ChfFile
{
    public const int Size = 4096;
    
    public ChfHeader Header;
    public ChfData Data;
    public ChfFooter Footer;
}