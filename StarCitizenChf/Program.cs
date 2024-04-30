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


var web = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins = web.Concat(local).ToArray();

const int offset = 0x44;
const int offset2 = 0x110;
const int offset3 = 0x13c;
const int offset4 = 0x140;
const int offset5 = 0x144;
const int offset6 = 0x108;


const int size = 4;
var data = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset).Take(size).ToArray()))).ToArray();
var data2 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset2).Take(size).ToArray()))).ToArray();
var data3 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset3).Take(size).ToArray()))).ToArray();
var data4 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset4).Take(size).ToArray()))).ToArray();
var data5 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset5).Take(size).ToArray()))).ToArray();
var data6 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset6).Take(size).ToArray()))).ToArray();
var data7 = allBins.Select(x => (x.name, BitConverter.ToString(x.data.Skip(offset6 + 4).Take(size).ToArray()))).ToArray();

var characters = allBins.Select(x =>  StarCitizenCharacter.FromBytes(x.name, x.data)).ToArray();

return;

//only search for colors in website characters since those have images we can compare to.
// var websiteRevs = Directory.GetFiles(folders.WebsiteCharacters, "*.rev", SearchOption.AllDirectories);
// await Task.WhenAll(websiteRevs.SelectMany(b => new[]
// {
//     ColorAnalyzer.FindColorsAtIndices(b),
//     ColorAnalyzer.FindColorsWithPattern(b, [0xBD, 0x53, 0x07, 0x97]),
//     ColorAnalyzer.FindColorsWithPattern(b, [0xA2, 0xC7, 0xC9, 0x09]),
// }));

// prints all data buffers to a file, one per line, in 4 byte chunks for easy visual comparison.
await Analysis.PrintAllToFileAsync(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories), Path.Combine(folders.Base, "bins.txt"));

//brute force search for common bytes in all files.
//this can be useful to find patterns in the data.
//Analysis.BruteForceCommonBytes(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories));