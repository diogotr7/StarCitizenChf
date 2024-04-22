using ZstdSharp;

namespace StarCitizenChf;

public static class Decompression
{
    public static void Decompress(string inputFolder, string outputFolder)
    {
        var files = Directory.GetFiles(inputFolder, "*.chf", SearchOption.AllDirectories)
            .Select(x => (Path.GetFileName(x), File.ReadAllBytes(x))).ToArray();

        using var zstd = new Decompressor();

        Directory.CreateDirectory("decompressed");
        foreach (var (name, buffer) in files)
        {
            var useful = buffer[16..^8].AsSpan();
            //find first 7 zero-bytes in a row
            var lastDataByte = FindLastDataByte(useful);
            if (lastDataByte == -1)
                continue;

            var cropped = useful[..lastDataByte];
            try
            {
                var decompressed = zstd.Unwrap(cropped.ToArray());

                Console.WriteLine($"Writing {decompressed.Length} bytes to {name}");
                File.WriteAllBytes(Path.Combine(outputFolder, Path.ChangeExtension(name, ".bin")), decompressed.ToArray());
            }
            catch (Exception e)
            {
                var desiredIndex = BruteForceDecompress(name, buffer);
                Console.WriteLine($"Failed to decompress {name}. Desired index: {desiredIndex}");
            }
        }
    }

    private static int BruteForceDecompress(string fileName, byte[] buffer)
    {
        var useful = buffer[16..^8].ToArray();

        for (var i = 16; i < useful.Length; i++)
        {
            try
            {
                using var zstd = new Decompressor();
                var decompressed = zstd.Unwrap(useful.AsSpan()[..i]);

                if (decompressed.Length == 0)
                    continue;

                File.WriteAllBytes(Path.Combine("decompressed", Path.ChangeExtension(fileName, ".bin")), decompressed.ToArray());
                return i + 16;
            }
            catch
            {
                //ignore
            }
        }

        return -1;
    }


    ///This method doesn't work for some files because the padding is not always deadbeef or zero bytes.
    private static int FindLastDataByteOld(ReadOnlySpan<byte> useful)
    {
        const int expectedLength = 4096 - 8 - 16;
        if (useful.Length != expectedLength)
            throw new ArgumentException("Span must be expectedLength bytes long", nameof(useful));
        //check for zstd magic number
        if (useful[0] != 0x28 || useful[1] != 0xB5 || useful[2] != 0x2F || useful[3] != 0xFD)
            throw new ArgumentException("Span must start with Zstd magic number", nameof(useful));

        var containsDeadbeef = useful.IndexOf<byte>([0xEF, 0xBE, 0xAD, 0xDE]) != -1;

        if (!containsDeadbeef)
            return useful.LastIndexOfAnyExcept<byte>(0x00) + 1;

        var firstDeadbeef = FindFirstDeadBeefByte(useful);
        var slice = useful[..firstDeadbeef];
        return slice.LastIndexOfAnyExcept<byte>(0x00) + 1;
    }

    private static int FindLastDataByte(ReadOnlySpan<byte> useful)
    {
        const int expectedLength = 4096 - 8 - 16;
        if (useful.Length != expectedLength)
            throw new ArgumentException("Span must be expectedLength bytes long", nameof(useful));

        //find first 7 zero bytes in a row
        return useful.IndexOf<byte>([0, 0, 0, 0, 0, 0, 0]);
    }

    private static int FindFirstDeadBeefByte(ReadOnlySpan<byte> useful)
    {
        const int expectedLength = 4096 - 8 - 16;
        if (useful.Length != expectedLength)
            throw new ArgumentException("Span must be expectedLength bytes long", nameof(useful));

        for (var i = useful.Length - 1; i >= 0; i -= 4)
        {
            if (useful[i] != 0xDE)
                return i;
            if (useful[i - 1] != 0xAD)
                return i - 1;
            if (useful[i - 2] != 0xBE)
                return i - 2;
            if (useful[i - 3] != 0xEF)
                return i - 3;
        }

        return -1;
    }
}