using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("watch-import", Description = "Watch for new characters in the Star Citizen folder and import them.")]
public class WatchImportCommand : ICommand
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
        watcher.Filter = "*.chf";
        
        watcher.Renamed += async (_, eventArgs) =>
        {
            await console.Output.WriteLineAsync($"New character detected: {eventArgs.FullPath}");
            await Processing.ProcessCharacter(eventArgs.FullPath);
            await console.Output.WriteLineAsync($"Character processed: {eventArgs.FullPath}");
        };

        watcher.EnableRaisingEvents = true;

        await console.Output.WriteLineAsync("Press enter to stop watching for new characters.");
        await console.Input.ReadLineAsync();
    }
}