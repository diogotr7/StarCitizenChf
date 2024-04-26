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

var femaleHotPink = Path.Combine(folders.ModdedCharacters, "female_black_eye_modded.bin");
var buffer = File.ReadAllBytes(femaleHotPink);
var file = new ChfFile(buffer);
var output = Path.ChangeExtension(femaleHotPink, ".chf");
File.WriteAllBytes(output, file.File);
