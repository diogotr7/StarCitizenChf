using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ChfParser;
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
    public static async Task ProcessCharacter(string chf)
    {
        if (!chf.EndsWith(".chf"))
            throw new ArgumentException("Not a chf file", nameof(chf));

        var bin = Path.ChangeExtension(chf, ".bin");
        if (!File.Exists(bin))
            await Decompression.DecompressFile(chf, bin);
        
        var json = Path.ChangeExtension(chf, ".json");
        //if (!File.Exists(json))
            await ExtractCharacterJson(bin, json);
    }

    public static async Task ProcessAllCharacters(string charactersFolder)
    {
        await Task.WhenAll(Directory.GetFiles(charactersFolder, "*.chf", SearchOption.AllDirectories).Select(async characterFile =>
        {
            try
            {
                await ProcessCharacter(characterFile);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing {characterFile}: {e.Message}");
            }
        }));
    }

    private static readonly JsonSerializerOptions opts = new() { WriteIndented = true, Converters = { new JsonStringEnumConverter() } };

    public static async Task ExtractCharacterJson(string inputFilename, string outputFilename)
    {
        var data = await File.ReadAllBytesAsync(inputFilename);
        var character = StarCitizenCharacter.FromBytes(data);
        var json = JsonSerializer.Serialize(character, opts);
        await File.WriteAllTextAsync(outputFilename, json);
    }

    public static void WatchForNewCharacters()
    {
        var inputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Roberts Space Industries", "StarCitizen", "EPTU", "user", "client", "0", "CustomCharacters");
        using var watcher = new FileSystemWatcher(inputFolder);
        watcher.NotifyFilter = NotifyFilters.Attributes |
                               NotifyFilters.CreationTime |
                               NotifyFilters.FileName |
                               NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.Size |
                               NotifyFilters.Security;
        
        watcher.Renamed += async (_, eventArgs) =>
        {
            Console.WriteLine($"2New character detected: {eventArgs.FullPath}");
            await ProcessCharacter(eventArgs.FullPath);
            Console.WriteLine($"Character processed: {eventArgs.FullPath}");
        };

        watcher.EnableRaisingEvents = true;

        Console.WriteLine("Press enter to stop watching for new characters.");
        Console.ReadLine();
    }

    public static string FixWeirdDnaString(string dna)
    {
        //384 = 48 uints * 4 bytes * 2 char per byte
        if (dna.Length != 48 * 4 * 2)
            throw new ArgumentException("Invalid dna length", nameof(dna));
        
        var stringBuilder = new StringBuilder();

        //reverse endianness
        for (var i = 0; i < 48; i++)
        {
            var start = i * 8;
            var part = dna.Substring(start, 8);
            stringBuilder.Append(part[6..8]);
            stringBuilder.Append(part[4..6]);
            stringBuilder.Append(part[2..4]);
            stringBuilder.Append(part[0..2]);
        }
        
        return stringBuilder.ToString();
    }
}