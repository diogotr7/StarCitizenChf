using System;
using System.Collections.Generic;
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
await Processing.ImportGameCharacters(folders.LocalCharacters);
await Processing.ConvertAllBinariesToChfAsync(folders.ModdedCharacters);

//Extracts all chf files into bins, reverses these bins for easier analysis.
//also tries to extract some color information to compare to images.
await Processing.ProcessAllCharacters(folders.WebsiteCharacters);
await Processing.ProcessAllCharacters(folders.LocalCharacters);

var web = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins = web.Concat(local).ToArray();

var characters = allBins.Select(x =>  (x.name, character: StarCitizenCharacter.FromBytes(x.data))).ToArray();
var charactersWithColor = characters.Select(t =>
{
    var (name, c) = t;
    var colors = new HashSet<Color>
    {
        c.CustomMaterial.Colors.HeadColor,
        c.CustomMaterial.Colors.Data01,
        c.CustomMaterial.Colors.Data02,
        c.CustomMaterial.Colors.Data03,
        c.CustomMaterial.Colors.Data04,
        c.CustomMaterial.Colors.Data05,
        c.CustomMaterial.Colors.Data06,
        c.CustomMaterial.Colors.Data07,
        c.CustomMaterial.Colors.Data08,
        c.CustomMaterial.Colors.Data09,
        c.EyeMaterial.EyeColor,
        c.BodyMaterial.TorsoColor,
        c.BodyMaterial.LimbColor
    };
    
    foreach (var prop in c.Props)
    {
        if (prop.Colors?.Color01 is { } x)
            colors.Add(x);
        if (prop.Colors?.Color02 is { } y)
            colors.Add(y);
    }
    
    return (name, c, colors);
}).ToArray();

await Task.WhenAll(charactersWithColor.Select(async character =>
{
    Console.WriteLine($"Processing {character.name}");
    //write all colors in the character to a properly named file in the character folder.
    var folder = Path.Combine(folders.ColorsFolder, Path.GetFileNameWithoutExtension(character.name)!);
    Directory.CreateDirectory(folder);
    await Task.WhenAll(character.colors.Select(async color =>
    {
        var path = Path.Combine(folder!, $"{color.ToString()}.png");
        
        if (File.Exists(path))
            return;
        
        await Utils.WriteSolidColorImage(path, color);
    }));
    
    Console.WriteLine($"Finished {character.name}");
}));
