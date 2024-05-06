using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfParser;
using ChfUtils;
using StarCitizenChf;
var inputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
    "Roberts Space Industries", "StarCitizen", "EPTU", "user", "client", "0", "CustomCharacters");
using var watcher = new FileSystemWatcher(inputFolder);
watcher.NotifyFilter = NotifyFilters.Attributes |
                       NotifyFilters.CreationTime |
                       NotifyFilters.FileName |
                       NotifyFilters.LastAccess |
                       NotifyFilters.LastWrite |
                       NotifyFilters.Size |
                       NotifyFilters.Security;
watcher.Renamed += async (sender, eventArgs) =>
{
    Console.WriteLine($"2New character detected: {eventArgs.FullPath}");
    await Processing.ProcessCharacter(eventArgs.FullPath);
    Console.WriteLine($"Character processed: {eventArgs.FullPath}");
};

watcher.EnableRaisingEvents = true;

Console.ReadLine();
return;

GuidUtils.Test();

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var folders = new Folders(csprojFolder);

//Downloads all characters from the website and saves them to the website characters folder.
//await Download.DownloadAllMetadata(folders.MetadataFile);
//await Download.DownloadAllCharacters(folders.MetadataFile, folders.WebsiteCharacters);

//Imports all non-modded characters exported from the game into our local characters folder.
await Processing.ImportGameCharacters(folders.LocalCharacters);
await Processing.ConvertAllBinariesToChfAsync(folders.ModdedCharacters);

//Extracts all chf files into bins, reverses these bins for easier analysis.
//also tries to extract some color information to compare to images.
await Processing.ProcessAllCharacters(folders.WebsiteCharacters);
await Processing.ProcessAllCharacters(folders.LocalCharacters);

var web = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins = web.Concat(local).ToArray();

var characters = allBins.Select(x =>  StarCitizenCharacter.FromBytes(x.data)).ToArray();

return;
