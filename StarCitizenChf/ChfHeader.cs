namespace StarCitizenChf;

public readonly struct ChfHeader
{
    public readonly uint Magic;
    public readonly uint Crc32c;
    public readonly uint CompressedSize;
    public readonly uint UncompressedSize;
}