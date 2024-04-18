using ZstdSharp;

AnalyzeSimilarities();
return;

static void AnalyzeSimilarities()
{
    var decompressedFiles = Directory.GetFiles("decompressed", "*.bin", SearchOption.AllDirectories)
        .Select(x => (Path.GetFileName(x), File.ReadAllBytes(x))).ToArray();

    var smallest = decompressedFiles.MinBy(x => x.Item2.Length).Item2.Length;
    //analyze byte by byte if it is the same in all files
    var commonBytes = new List<int>();
    for (var i = 0; i < smallest; i++)
    {
        if (decompressedFiles.All(b => b.Item2[i] == decompressedFiles[0].Item2[i]))
            commonBytes.Add(i);
    }
    var valuesAtCommonBytes = commonBytes.Select(i => decompressedFiles[0].Item2[i]).ToArray();
    var commonBytesString = string.Join(", ", commonBytes.Select(i => $"0x{i:X2}"));
    var commonValuesString = string.Join(", ", valuesAtCommonBytes.Select(i => $"0x{i:X2}"));
    
    //compute sequences of bytes in a row. This is useful for finding patterns in the data
    //input: 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15
    //output: 0-3, 5-10, 12-15
    var sequences = new List<(int, int)>();
    for (var i = 0; i < commonBytes.Count; i++)
    {
        var start = commonBytes[i];
        var end = start;
        while (i + 1 < commonBytes.Count && commonBytes[i + 1] == end + 1)
        {
            end++;
            i++;
        }

        sequences.Add((start, end));
    }
    var sequencesString = string.Join(", ", sequences.Select(x => $"0x{x.Item1:X4}-0x{x.Item2:X4}"));
    
    Console.WriteLine($"Common sequences: {sequencesString}");
    Console.WriteLine($"Common bytes: {commonBytesString}");
    Console.WriteLine($"Values at common bytes: {commonValuesString}");
}

static void Decompress()
{
    var files = Directory.GetFiles("data", "*.chf", SearchOption.AllDirectories)
        .Select(x => (Path.GetFileName(x), File.ReadAllBytes(x))).ToArray();

    Directory.CreateDirectory("decompressed");
    foreach (var (name, buffer) in files)
    {
        try
        {
            var useful = buffer[16..^8];
            var lastDataByte = FindLastDataByte(useful);
            if (lastDataByte == -1)
                continue;

            var cropped = useful[..(lastDataByte + 1)];
            using var zstd = new Decompressor();
            var decompressed = zstd.Unwrap(cropped.ToArray());

            Console.WriteLine($"Decompressed length: {decompressed.Length}");
            File.WriteAllBytes(Path.Combine("decompressed", Path.ChangeExtension(name, ".bin")), decompressed.ToArray());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

static int FindLastDataByte(ReadOnlySpan<byte> useful)
{
    const int expectedLength = 4096 - 8 - 16;
    if (useful.Length != expectedLength) throw new ArgumentException("Span must be expectedLength bytes long", nameof(useful));
    //check for zstd magic number
    if (useful[0] != 0x28 || useful[1] != 0xB5 || useful[2] != 0x2F || useful[3] != 0xFD)
        throw new ArgumentException("Span must start with Zstd magic number", nameof(useful));

    var containsDeadbeef = useful.IndexOf<byte>([0xEF, 0xBE, 0xAD, 0xDE]) != -1;

    if (!containsDeadbeef)
        return useful.LastIndexOfAnyExcept<byte>(0x00);

    var firstDeadbeef = FindFirstDeadBeefByte(useful);
    var slice = useful[..firstDeadbeef];
    return slice.LastIndexOfAnyExcept<byte>(0x00);
}

static int FindFirstDeadBeefByte(ReadOnlySpan<byte> useful)
{
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