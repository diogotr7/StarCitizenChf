using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace StarCitizenChf;

public static class Utils
{
    public static (byte[] data, string name)[] LoadFilesWithNames(string path, string pattern = "*")
    {
        return Directory.GetFiles(path, pattern, SearchOption.AllDirectories)
            .Select(x => (File.ReadAllBytes(x), x))
            .Where(x => x.Item1.Length > 0)
            .ToArray();
    }

    public static string GetSafeDirectoryName(Character character)
    {
        var start = character.title;

        Array.ForEach([..Path.GetInvalidPathChars(), ' '], x => start = start.Replace(x, '_'));

        return $"{start}-{character.id[..8]}";
    }

    public static async Task ImportGameCharacters(string outputFolder)
    {
        var inputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Roberts Space Industries", "StarCitizen", "EPTU", "user", "client", "0", "CustomCharacters");

        var characters = Directory.GetFiles(inputFolder, "*.chf", SearchOption.AllDirectories);

        foreach (var character in characters)
        {
            var name = Path.GetFileNameWithoutExtension(character);
            var output = Path.Combine(outputFolder, name);

            if (Directory.Exists(output))
                continue;
            
            var chf = ChfFile.FromChf(character);
            
            //do not import our own characters again.
            if (chf.IsModded())
                continue;

            Directory.CreateDirectory(output);
            await chf.WriteToFileAsync(Path.Combine(output, $"{name}.chf"));
        }
    }
    
    public static async Task ConvertAllBinariesToChfAsync(string folder)
    {
        var bins = Directory.GetFiles(folder, "*.bin", SearchOption.AllDirectories);
        await Task.WhenAll(bins.Select(async b =>
        {
            var target = Path.ChangeExtension(b, ".chf");
            if (File.Exists(target))
                return;
            
            var file = ChfFile.FromBin(b);
            await file.WriteToFileAsync(target);
        }));
    }

    public static async Task ReverseFile(string bin, string reversedBin)
    {
        var data = await File.ReadAllBytesAsync(bin);
        Array.Reverse(data);
        await File.WriteAllBytesAsync(reversedBin, data);
    }
    
    public static async Task WriteSolidColorImage(string path, ChfParser.Color color)
    {
        var c = new Rgba32()
        {
            R = color.R,
            G = color.G,
            B = color.B,
            A = 255
        };
        using var image = new Image<Rgba32>(256, 256, c);
        await using var stream = File.OpenWrite(path);
        await image.SaveAsPngAsync(stream);
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