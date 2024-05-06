using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfParser;
using ChfUtils;
using StarCitizenChf;

GuidUtils.Test();

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var folders = new Folders(csprojFolder);

//Downloads all characters from the website and saves them to the website characters folder.
//await Download.DownloadAllMetadata(folders.MetadataFile);
//await Download.DownloadAllCharacters(folders.MetadataFile, folders.WebsiteCharacters);

//Imports all non-modded characters exported from the game into our local characters folder.
await Utils.ImportGameCharacters(folders.LocalCharacters);
await Utils.ConvertAllBinariesToChfAsync(folders.ModdedCharacters);

//Extracts all chf files into bins, reverses these bins for easier analysis.
//also tries to extract some color information to compare to images.
await Processing.ProcessAllCharacters(folders.WebsiteCharacters);
await Processing.ProcessAllCharacters(folders.LocalCharacters);

var web = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins = web;

var characters = allBins.Select(x =>  StarCitizenCharacter.FromBytes(x.name, x.data)).ToArray();
return;
await Task.WhenAll(characters.Select(async character =>
{
    Console.WriteLine($"Processing {character.Name}");
    //write all colors in the character to a properly named file in the character folder.
    var folder = Path.GetDirectoryName(character.Name);
    Directory.CreateDirectory(folder);
    await Task.WhenAll(character.AllColors.Select(async color =>
    {
        var path = Path.Combine(folder, $"{color.ToString()}.png");
        
        if (File.Exists(path))
            return;
        
        await Utils.WriteSolidColorImage(path, color);
    }));
    
    Console.WriteLine($"Finished {character.Name}");
}));

