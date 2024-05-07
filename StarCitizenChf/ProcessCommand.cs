using System;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command]
//default is process single just so i can drag and drop a file on the exe.
public class ProcessCommand : ICommand
{
    [CommandParameter(0, Description = "Character file")]
    public string? CharacterFile { get; set; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        if (string.IsNullOrWhiteSpace(CharacterFile))
        {
            await console.Error.WriteLineAsync("Character file is required");
            return;
        }

        try
        {
            await Processing.ProcessCharacter(CharacterFile);
            await console.Output.WriteLineAsync($"Processed {CharacterFile}");
        }
        catch (Exception e)
        {
            await console.Error.WriteLineAsync($"Error processing {CharacterFile}: {e.Message}");
        }
    }
}