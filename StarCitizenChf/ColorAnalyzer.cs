namespace StarCitizenChf;

public static class ColorAnalyzer
{
    private static readonly int[] _colorIndices =
    [
        0x04,
        0x2c,
        0x84,
        0xdc,
        0xe8,
        0x194,
        0x19c,
        0x24c,
        0x258,
        0x284,
        0x348,
    ];
    
    public static async Task ExtractCharacterColors(string file)
    {
        if (!file.EndsWith(".reversed.bin"))
            throw new ArgumentException("File must be a .bin file");

        var bytes = await File.ReadAllBytesAsync(file);
        
        int i = 0;
        foreach (var idx in _colorIndices)
        {
            //image i should always be relative to the array even if the color is not found
            var imageFile = Path.Combine(Path.GetDirectoryName(file)!, $"image_{i++}.png");

            var rgba = bytes.AsSpan().Slice(idx, 4).ToArray();

            // if (rgba[0] != 0xFF)
            // {
            //     Console.WriteLine($"[{file}] Skipping {idx:X2} because alpha is not 0xFF");
            //     continue;
            // }

            
            var color = new Rgba32(rgba[3], rgba[2], rgba[1], rgba[0]);
            
            await Images.WriteSolidColorImage(imageFile, color);
            
            Console.WriteLine($"#{rgba[3]:X2}{rgba[2]:X2}{rgba[1]:X2}{rgba[0]:X2}");
        }

    }
    
    public static async Task GetHairColorDye(string file)
    {
        if (!file.EndsWith(".reversed.bin"))
            throw new ArgumentException("File must be a .bin file");

        var bytes = await File.ReadAllBytesAsync(file);

        var key = new byte[]{0xA2, 0xC7, 0xC9, 0x09};
        var idx = bytes.AsSpan().IndexOf(key);
        if (idx == -1)
        {
            return;
        }
        
        var rgba = bytes.AsSpan().Slice(idx + 4 + 4, 4).ToArray();
        var color = new Rgba32(rgba[3], rgba[2], rgba[1], rgba[0]);
        
        var imageFile = Path.Combine(Path.GetDirectoryName(file)!, "hair_dye.png");
        await Images.WriteSolidColorImage(imageFile, color);
    }
    
    public static void FindAllColors(string file)
    {
        var magic = new byte[] { 0xBD, 0x53, 0x07, 0x97 };
        var bytes = File.ReadAllBytes(file);
        var colors = new List<Rgba32>();
        var asd = bytes.AsSpan().IndexOfAll(magic).ToArray();
        
        foreach (var idx in asd)
        {
            var rgba = bytes.AsSpan().Slice(idx - 4, 4).ToArray();
            var color = new Rgba32(rgba[3], rgba[2], rgba[1], rgba[0]);
            colors.Add(color);
        }
        
        int i = 0;
        foreach (var color in colors)
        {
            var imageFile = Path.Combine(Path.GetDirectoryName(file)!, $"autocolor_{i++}.png");
            Images.WriteSolidColorImage(imageFile, color).Wait();
        }
    }
    
    public static List<int> IndexOfAll(this Span<byte> source, ReadOnlySpan<byte> pattern)
    {
        var indices = new List<int>();
        
        var index = source.IndexOf(pattern);
        while (index != -1)
        {
            indices.Add(index);
            var newIndex = source[(index + pattern.Length)..].IndexOf(pattern);
            if (newIndex == -1)
                break;
            
            index += newIndex + pattern.Length;
        }
        
        return indices;
    }
}