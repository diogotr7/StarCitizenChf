using System.IO;
using System.Threading.Tasks;
using ChfParser;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("watch-export", Description = "Watch for new modded characters and export them to the star citizen folder.")]
public class WatchExportCommand : ICommand
{
    [CommandOption("input", 'i', Description = "Input folder")]
    public string InputFolder { get; set; } = Folders.ModdedCharacters;
    
    public async ValueTask ExecuteAsync(IConsole console)
    {
        using var watcher = new FileSystemWatcher(InputFolder);
        watcher.NotifyFilter = NotifyFilters.Attributes |
                               NotifyFilters.CreationTime |
                               NotifyFilters.FileName |
                               NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.Size |
                               NotifyFilters.Security;
        watcher.Filter = "*.bin";
        
        watcher.Renamed += async (_, eventArgs) =>
        {
            if (eventArgs.Name  == null)
                return;
            
            await console.Output.WriteLineAsync($"New character detected: {eventArgs.FullPath}");
            
            var target = Path.Combine(Folders.StarCitizenCharactersFolder, Path.ChangeExtension(eventArgs.Name, ".chf"));
            
            if (File.Exists(target))
                return;
            
            var chfFile = ChfFile.FromBin(eventArgs.FullPath);
            await chfFile.WriteToChfFileAsync(target);
            
            await console.Output.WriteLineAsync($"Character Exported: {eventArgs.FullPath}");
        };

        watcher.EnableRaisingEvents = true;

        await console.Output.WriteLineAsync("Press enter to stop watching for new characters.");
        await console.Input.ReadLineAsync();
    }
}