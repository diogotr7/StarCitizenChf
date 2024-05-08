using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfParser;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("export-all", Description = "Exports all modded characters into the Star Citizen folder.")]
public class ExportAllCommand : ICommand
{
    [CommandOption("input", 'i', Description = "Input folder")]
    public string InputFolder { get; set; } = Folders.ModdedCharacters;

    [CommandOption("output", 'o', Description = "Output folder")]
    public string OutputFolder { get; set; }  = Folders.StarCitizenCharactersFolder;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        if (!Directory.Exists(InputFolder))
        {
            await console.Error.WriteLineAsync($"{InputFolder} not found");
            return;
        }

        var bins = Directory.GetFiles(InputFolder, "*.bin", SearchOption.AllDirectories);
        await Task.WhenAll(bins.Select(async b =>
        {
            var target = Path.Combine(OutputFolder, Path.ChangeExtension(b, ".chf"));
            if (File.Exists(target))
                return;

            var file = ChfFile.FromBin(b);
            
            await console.Output.WriteLineAsync($"Exporting {Path.GetFileNameWithoutExtension((string?)b)}");
            await file.WriteToChfFileAsync(target);
        }));
    }
}