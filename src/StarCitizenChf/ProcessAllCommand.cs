using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("process-all", Description = "Processes all characters in the given folder.")]
public class ProcessAllCommand : ICommand
{
    [CommandOption("input", 'i', Description = "Input Folder to process.")]
    public string InputFolder { get; set; } = Folders.WebsiteCharacters;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        if (!Directory.Exists(InputFolder))
        {
            await console.Error.WriteLineAsync("Folder not found");
            return;
        }
        
        foreach (var characterFile in Directory.GetFiles(InputFolder, "*.chf", SearchOption.AllDirectories))
        {
            try
            {
                await Processing.ProcessCharacter(characterFile);
                await console.Output.WriteLineAsync($"Processed {characterFile}");
            }
            catch (Exception e)
            {
                await console.Error.WriteLineAsync($"Error processing {characterFile}: {e.Message}");
            }
        }
    }
}