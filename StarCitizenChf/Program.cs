using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StarCitizenChf;

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

//only search for colors in website characters since those have images we can compare to.
var websiteRevs = Directory.GetFiles(folders.WebsiteCharacters, "*.rev", SearchOption.AllDirectories);
await Task.WhenAll(websiteRevs.SelectMany(b => new[]
{
    ColorAnalyzer.FindColorsAtIndices(b),
    ColorAnalyzer.FindColorsWithPattern(b, [0xBD, 0x53, 0x07, 0x97]),
    ColorAnalyzer.FindColorsWithPattern(b, [0xA2, 0xC7, 0xC9, 0x09]),
}));

// prints all data buffers to a file, one per line, in 4 byte chunks for easy visual comparison.
await Analysis.PrintAllToFileAsync(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories), Path.Combine(folders.Base, "bins.txt"));

//brute force search for common bytes in all files.
//this can be useful to find patterns in the data.
Analysis.BruteForceCommonBytes(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories));
