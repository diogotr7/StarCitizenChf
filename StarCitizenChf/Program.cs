using System.Text.Json;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var dataFolder = Path.Combine(csprojFolder, "data");
var metadataFile = Path.Combine(dataFolder, "characters.json");
var charactersFolder = Path.Combine(dataFolder, "websiteCharacters");
var localCharactersFolder = Path.Combine(dataFolder, "localCharacters");

//Utils.ImportGameCharacters(localCharactersFolder);

//download files.json from the star citizen characters website
//await Download.DownloadAllMetadata(metadataFile);

//download images and chf files for all characters, skip if already downloaded
//await Download.DownloadAllCharacters(JsonSerializer.Deserialize<Character[]>(File.ReadAllText(metadataFile))!, charactersFolder);

//decompress all chf files and write their hex views, skip if already done
await Task.WhenAll([
    Processing.ProcessAllCharacters(charactersFolder),
    Processing.ProcessAllCharacters(localCharactersFolder)
]);

var x = Analysis.AnalyzeSimilarities(Directory.GetFiles(dataFolder, "*.chf", SearchOption.AllDirectories));

return;
var default_m = Path.Combine(localCharactersFolder, "default_m", "default_m.chf");
var dest = Path.Combine(localCharactersFolder, "default_m", "default_m_to_f.chf");
await Decompression.MutateFile(default_m, dest, x =>
{
    Mutations.ChangeBodyType(x);
});

foreach (var name in Directory.GetFiles(charactersFolder, "*.bin", SearchOption.AllDirectories))
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