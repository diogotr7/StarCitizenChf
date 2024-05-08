using System;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("process", Description = "Process a character file")]
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