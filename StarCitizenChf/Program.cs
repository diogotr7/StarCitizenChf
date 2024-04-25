using System.Buffers;
using System.Runtime.InteropServices;
using System.Text.Json;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var dataFolder = Path.Combine(csprojFolder, "data");
var metadataFile = Path.Combine(dataFolder, "characters.json");
var charactersFolder = Path.Combine(dataFolder, "websiteCharacters");
var localCharactersFolder = Path.Combine(dataFolder, "localCharacters");

//Utils.ImportGameCharacters(localCharactersFolder);

await Download.DownloadAllMetadata(metadataFile);

await Download.DownloadAllCharacters(JsonSerializer.Deserialize<Character[]>(File.ReadAllText(metadataFile))!, charactersFolder);

await Task.WhenAll([
    Processing.ProcessAllCharacters(charactersFolder),
    Processing.ProcessAllCharacters(localCharactersFolder)
]);

var default_m = Path.Combine(localCharactersFolder, "default_m", "default_m.chf");
var buffer = await File.ReadAllBytesAsync(default_m);
var file = MemoryMarshal.AsRef<ChfFile>(buffer.AsSpan());

var dest = Path.Combine(localCharactersFolder, "default_m", "default_m_to_f.chf");
await Decompression.MutateFile(default_m, dest, x =>
{
    Mutations.ChangeBodyType(x);
});
//todo: this is not working. The game does not complain about the crc but crashes when loading the character