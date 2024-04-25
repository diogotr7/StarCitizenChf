using System.Text.Json;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var dataFolder = Path.Combine(csprojFolder, "data");
var metadataFile = Path.Combine(dataFolder, "characters.json");
var charactersFolder = Path.Combine(dataFolder, "websiteCharacters");
var localCharactersFolder = Path.Combine(dataFolder, "localCharacters");
var moddedCharactersFolder = Path.Combine(dataFolder, "moddedCharacters");
var tempFolder = Path.Combine(dataFolder, "temp");
foreach (var folder in new[] { charactersFolder, localCharactersFolder, moddedCharactersFolder, tempFolder })
    Directory.CreateDirectory(folder);

Directory.EnumerateFiles(tempFolder).ToList().ForEach(File.Delete);

Decryption.DecryptAll(dataFolder);


var default_f = Path.Combine(localCharactersFolder, "default_f", "default_f.bin");
var s = Convert.ToBase64String(File.ReadAllBytes(default_f));

var moddedBin = Path.Combine(moddedCharactersFolder, "default_f", "default_f.bin");
var bytes = File.ReadAllBytes(moddedBin);
var output = new ChfFile(bytes);

File.WriteAllBytes(Path.Combine(moddedCharactersFolder, "default_f", "default_f_out_3.chf"), output.File);

Utils.ImportGameCharacters(localCharactersFolder);

await Download.DownloadAllMetadata(metadataFile);

await Download.DownloadAllCharacters(JsonSerializer.Deserialize<Character[]>(File.ReadAllText(metadataFile))!, charactersFolder);

await Task.WhenAll([
    Processing.ProcessAllCharacters(charactersFolder),
    Processing.ProcessAllCharacters(localCharactersFolder)
]);


var default_m = Path.Combine(localCharactersFolder, "default_m", "default_m.chf");
var dest = Path.Combine(moddedCharactersFolder, "default_m", "default_m_to_f.chf");
await Decompression.MutateFile(default_m, dest, x =>
{
    Mutations.ChangeBodyType(x);
});
//todo: this is not working. The game does not complain about the crc but crashes when loading the character