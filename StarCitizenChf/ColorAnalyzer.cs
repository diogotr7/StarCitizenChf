using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;

namespace StarCitizenChf;

public static class ColorAnalyzer
{
    private static readonly int[] _colorIndices =
    [
        0x04,
        0x2c,
        0x84,
        0x88,
        0xDC,
        0xE0,
        0xE8,
        0x194,
        0x19c,
        0x24c,
        0x258,
        0x284,
        0x348,
    ];

    public static async Task FindColorsAtIndices(string file)
    {
        if (!file.EndsWith(".rev"))
            throw new ArgumentException("File must be a .rev file");

        var bytes = await File.ReadAllBytesAsync(file);
        if (bytes.Length == 0)
            return;
        
        
        int i = 0;
        foreach (var idx in _colorIndices)
        {
            //image i should always be relative to the array even if the color is not found
            var imageFile = Path.Combine(Path.GetDirectoryName(file)!, $"image_{i++}.png");

            var rgba = bytes.AsSpan().Slice(idx, 4).ToArray();

            var color = new Rgba32(rgba[3], rgba[2], rgba[1], 255);

            await Utils.WriteSolidColorImage(imageFile, color);
        }
    }

    public static async Task FindColorsWithPattern(string file, byte[] pattern)
    {
        if (!file.EndsWith(".rev"))
            throw new ArgumentException("File must be a .rev file");

        var patternNameFriendly = BitConverter.ToString(pattern).Replace("-", "");
        var bytes = await File.ReadAllBytesAsync(file);
        var colorLocations = bytes.AsSpan().IndexOfAll(pattern).ToArray();

        int i = 0;
        foreach (var colorLocation in colorLocations)
        {
            var rgba = bytes.AsSpan().Slice(colorLocation - 4, 4).ToArray();
            var color = new Rgba32(rgba[3], rgba[2], rgba[1]);
            var imageFile = Path.Combine(Path.GetDirectoryName(file)!, $"{patternNameFriendly}_{i++}.png");
            await Utils.WriteSolidColorImage(imageFile, color);
        }
    }
}