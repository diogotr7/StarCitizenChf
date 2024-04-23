using System.Text.Json;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var charactersFolder = Path.Combine(csprojFolder, "characters");

//await Download.DownloadAllMetadata(csprojFolder);
await Download.DownloadAllCharacters(JsonSerializer.Deserialize<Character[]>(File.ReadAllText(Path.Combine(csprojFolder, "total.json")))!, charactersFolder);

var characterFolders = Directory.GetDirectories(charactersFolder);
await Task.WhenAll(characterFolders.Select(async characterFolder =>
{
    try
    {
        var files = Directory.GetFiles(characterFolder);
        if (files.Length == 0)
            return;
        
        var chf = files.SingleOrDefault(x => x.EndsWith(".chf"));
        if (chf == null)
            return;
        var bin = Path.ChangeExtension(chf, ".bin");

        if (!File.Exists(bin))
            await Decompression.Decompress(chf, bin);

        var hex = Path.ChangeExtension(chf, ".txt");
        //if (!File.Exists(hex))
        await Processing.ConvertToHexView(bin, hex, 1);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}));

var chfFiles = Directory.GetFiles(charactersFolder, "*.chf", SearchOption.AllDirectories);
var hexFiles = Directory.GetFiles(charactersFolder, "*.txt", SearchOption.AllDirectories);
var binFiles = Utils.LoadFilesWithNames(charactersFolder, "*.bin");

foreach (var (data, name) in binFiles)
{
    try
    {
        var male = Analysis.IsMale(data);
        Console.WriteLine($"{name} is {(male ? "male" : "female")}");
    }
    catch
    {
        Console.WriteLine($"{name} is unknown");
    }
}