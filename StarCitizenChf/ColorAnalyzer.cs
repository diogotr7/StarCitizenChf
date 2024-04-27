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
            
            await Images.WriteSolidColorImage(imageFile, 256,256, color);
            
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
        await Images.WriteSolidColorImage(imageFile, 256,256, color);
    }
    
    

    const int a = 1065243000;
    const int b = 1065170376;
    const int c = 1064917280;
    const int d = 1064845615;
    const int e = 49768721;
}