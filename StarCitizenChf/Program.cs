var csprojFolder = Path.GetFullPath(@"..\..\..\");
var folders = new Folders(csprojFolder);

Utils.ImportGameCharacters(folders.LocalCharacters);

//await Download.DownloadAllMetadata(folders.MetadataFile);

//await Download.DownloadAllCharacters(folders.MetadataFile, folders.WebsiteCharacters);

await Task.WhenAll([
    Processing.ProcessAllCharacters(folders.WebsiteCharacters),
    Processing.ProcessAllCharacters(folders.LocalCharacters)
]);

//Analysis.BruteForceCommonBytes(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories));

var lightblue = Directory.GetFiles(folders.LocalCharacters, "lightblue*.bin", SearchOption.AllDirectories);

var ints = lightblue.Select(l =>
{
    var file = File.ReadAllBytes(l);
    ReadOnlySpan<byte> lb = [0x9c, 0xbe, 0xe7, 0xff];
    var idx = file.AsSpan().IndexOf(lb);
    return (Index: idx, DistanceFromEnd: file.Length - idx);
}).ToArray();//always 136 bytes from the end

//get 4 byte sequence 136 bytes from the end of every file
var allBins = Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories);
var allBytes = allBins.Select(File.ReadAllBytes).ToArray();
var lastBytes = allBytes.Select(b =>
{
    var rgba = b.AsSpan().Slice(b.Length - 136, 4).ToArray();
    return $"#{rgba[0]:X2}{rgba[1]:X2}{rgba[2]:X2}{rgba[3]:X2}";
}).Distinct().Order().ToArray();

var all = string.Join(Environment.NewLine, lastBytes);

return;

var femaleHotPink = Path.Combine(folders.ModdedCharacters, "female_black_eye_modded.bin");
var buffer = File.ReadAllBytes(femaleHotPink);
var file = new ChfFile(buffer);
var output = Path.ChangeExtension(femaleHotPink, ".chf");
File.WriteAllBytes(output, file.File);
