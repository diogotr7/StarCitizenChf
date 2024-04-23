using System.Text.Json;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var charactersFolder = Path.Combine(csprojFolder, "characters");
var websiteCharactersFolder = Path.Combine(csprojFolder, "websiteCharacters");
var localCharactersFolder = Path.Combine(csprojFolder, "localCharacters");

Utils.ImportGameCharacters(localCharactersFolder);

//download files.json from the star citizen characters website
//await Download.DownloadAllMetadata(csprojFolder);

//download images and chf files for all characters, skip if already downloaded
//await Download.DownloadAllCharacters(JsonSerializer.Deserialize<Character[]>(File.ReadAllText(Path.Combine(csprojFolder, "total.json")))!, websiteCharactersFolder);

//decompress all chf files and write their hex views, skip if already done
//await Processing.ProcessAllCharacters(websiteCharactersFolder);
await Processing.ProcessAllCharacters(localCharactersFolder);
return;
var chfFiles = Directory.GetFiles(charactersFolder, "*.chf", SearchOption.AllDirectories);
var hexFiles = Directory.GetFiles(charactersFolder, "*.txt", SearchOption.AllDirectories);
var binFiles = Directory.GetFiles(charactersFolder, "*.bin", SearchOption.AllDirectories);

foreach (var name in binFiles)
{
    try
    {
        Decryption.Decrypt(name, Path.ChangeExtension(name, "decrypted"));
    }
    catch
    {
        Console.WriteLine($"{name} failed");
    }
}