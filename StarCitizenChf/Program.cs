using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ChfParser;
using ChfUtils;
using SixLabors.ImageSharp.PixelFormats;
using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var folders = new Folders(csprojFolder);


var web1 = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local1 = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins1 = web1.Concat(local1).Select(x => x.data).ToArray();


//opt1 and opt2 always appear before presentAlways.
//opt1 always appears before opt2.
var opt1Bytes = BitConverter.GetBytes(0x6C836947);//basically all of them
var opt2Bytes = BitConverter.GetBytes(0x9B274D93);//bald, tate and boredgamerbald
var opt3Bytes = BitConverter.GetBytes(0xC3370BD9);

//might be a material key not related to optional data like facial hair or eyebrows etc.
var presentAlwaysBytes = BitConverter.GetBytes(0xA047885E);

var useful2 = allBins1.Where(l => Contains(l, opt1Bytes) && Contains(l, opt2Bytes)).ToArray();

var offsets2 = useful2.Select(y =>
{
    var x = y.AsSpan();
    //find how many bytes are between opt1 and opt2
    var index1 = x.IndexOf(opt1Bytes);
    var index2 = x.IndexOf(opt2Bytes);
    var index3 = x.IndexOf(presentAlwaysBytes);
    Debug.Assert(index1 < index2);
    Debug.Assert(index2 < index3);
    return (index3-index2, index2-index1);
}).ToArray();

//order of appearence:
//opt1, optional
//opt2, optional
//presentAlways, always present

//1 *never* appears after 2
var zzzzz = allBins1.Where(l => Contains(l, opt1Bytes, opt2Bytes, opt3Bytes)).ToArray();
var xxxx = allBins1.Where(l => Contains(l, opt2Bytes, opt3Bytes, opt1Bytes)).ToArray();
var sdfgsdf = allBins1.Where(l => Contains(l, opt2Bytes, opt1Bytes)).ToArray();
var zxvxcv = allBins1.Where(l => Contains(l, opt1Bytes, opt2Bytes)).ToArray();

//444
var a441 = allBins1.Where(l => Contains(l, opt1Bytes)).ToArray();
var a288 = allBins1.Where(l => Contains(l, opt2Bytes)).ToArray();
var asaaasd = allBins1.Where(l => Contains(l, opt3Bytes)).ToArray();
var a286 = allBins1.Where(l => Contains(l, opt1Bytes, opt2Bytes)).ToArray();
var asdasd = allBins1.Where(l => Contains(l, opt1Bytes) && !Contains(l, opt2Bytes)).ToArray();
var asdasd2 = allBins1.Where(l => Contains(l, opt2Bytes) && !Contains(l, opt1Bytes)).ToArray();

GuidUtils.Test();

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

// var threecolors = Path.Combine(folders.ModdedCharacters, "female_3colors","female_3colors.bin");
// var threecolorsBin = File.ReadAllBytes(threecolors);
// var threecolorsCharacter = StarCitizenCharacter.FromBytes(threecolors, threecolorsBin);

var web = Utils.LoadFilesWithNames(folders.WebsiteCharacters, "*.bin");
var local = Utils.LoadFilesWithNames(folders.LocalCharacters, "*.bin");
var allBins = web.Concat(local).ToArray();
HashSet<string> reversed = new();
foreach (var (data, name) in allBins)
{
    reversed.Add($"{BitConverter.ToString(data.Reverse().Skip(BodyMaterialInfo.Size).ToArray())} {name}");
}
File.WriteAllLines(Path.Combine(folders.Base, "reversed1.txt"), reversed.Order().OrderBy(l => l.Length));

HashSet<string> bins = new();
foreach (var bin in allBins)
{
    bins.Add($"{BitConverter.ToString(bin.data)} {bin.name}");
}
File.WriteAllLines(Path.Combine(folders.Base, "bins.txt"), bins.Order().OrderBy(l => l.Length));

var characters = allBins.Select(x =>  StarCitizenCharacter.FromBytes(x.name, x.data)).ToArray();

var idks = StarCitizenCharacter.idks.Where(x => x.Item2.Length > 0).Select(x => $"{BitConverter.ToString(x.Item2)} {x.Item1}").OrderBy(x => x.Length);
var idks_reversed = StarCitizenCharacter.idks.Where(x => x.Item2.Length > 0).Select(x => $"{BitConverter.ToString(x.Item2.Reverse().ToArray())} {x.Item1}").OrderBy(x => x.Length);
File.WriteAllLines(Path.Combine(folders.Base, "idks.txt"), idks);
File.WriteAllLines(Path.Combine(folders.Base, "idks_reversed.txt"), idks_reversed);
//
// var colors = characters.SelectMany<StarCitizenCharacter, Color>(x => [x.CustomColor]).Distinct().ToArray();
//  var folder = Path.Combine(folders.Base, "colors3");
//  Directory.CreateDirectory(folder);
//  int i = 0;
//  foreach (var color in colors)
//  {
//      var c = new Rgba32()
//      {
//          R = color.R,
//          G = color.G,
//          B = color.B,
//          A = 255
//      };
//      await Utils.WriteSolidColorImage(Path.Combine(folder, $"{i++}.png"), c);
//  }


return;
HashSet<string> remaining = new();
for (var index = 0; index < characters.Length; index++)
{
    var character = characters[index];
    var last = character.LastReadIndex;
    var data = allBins[index].data;
    
    remaining.Add($"{BitConverter.ToString(data[last..])} {character.Name}");
}
File.WriteAllLines(Path.Combine(folders.Base, "remaining.txt"), remaining.Order().OrderBy(l => l.Length));


return;

// prints all data buffers to a file, one per line, in 4 byte chunks for easy visual comparison.
await Analysis.PrintAllToFileAsync(Directory.GetFiles(folders.Base, "*.bin", SearchOption.AllDirectories), Path.Combine(folders.Base, "bins.txt"));

bool Contains(byte[] data, params byte[][] parts)
{
    Span<byte> search = data;
    foreach (var part in parts)
    {
        var index = search.IndexOf(part);
        if (index == -1)
            return false;
        search = search[(index + part.Length)..];
    }
    return true;
}