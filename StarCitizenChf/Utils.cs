using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace StarCitizenChf;

public static class Utils
{
    public static IEnumerable<(byte[] data, string name)> LoadFilesWithNames(string path, string pattern = "*")
    {
        return Directory.GetFiles(path, pattern, SearchOption.AllDirectories)
            .Select(x => (File.ReadAllBytes(x), x))
            .Where(x => x.Item1.Length > 0);
    }

    public static string GetSafeDirectoryName(Character character)
    {
        var start = character.title;

        Array.ForEach([..Path.GetInvalidPathChars(), ' '], x => start = start.Replace(x, '_'));

        return $"{start}-{character.id[..8]}";
    }

    public static async Task WriteSolidColorImage(string path, ChfParser.Color color)
    {
        using var image = new Image<Rgba32>(64, 64, new Rgba32
        {
            R = color.R,
            G = color.G,
            B = color.B,
            A = 255
        });
        await using var stream = File.OpenWrite(path);
        await image.SaveAsPngAsync(stream);
    }
}