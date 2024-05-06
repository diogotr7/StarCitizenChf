using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfUtils;

namespace StarCitizenChf;

public static class Processing
{
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

    public static async Task ProcessAllCharacters(string charactersFolder)
    {
        await Task.WhenAll(Directory.GetDirectories(charactersFolder).Select(async characterFolder =>
        {
            try
            {
                var files = Directory.GetFiles(characterFolder);
                if (files.Length == 0)
                    return;

                var chf = files.SingleOrDefault(x => x.EndsWith(".chf"));
                if (chf == null)
                    return;

                var bin = Path.ChangeExtension(chf, ".bin");
                if (!File.Exists(bin))
                    await Decompression.DecompressFile(chf, bin);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing {characterFolder}: {e.Message}");
            }
        }));
    }
}