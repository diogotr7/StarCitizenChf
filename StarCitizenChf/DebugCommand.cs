using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfParser;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace StarCitizenChf;

[Command("debug")]
public class DebugCommand : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        //using the string as is crashes the game.
        //here we process it in two different ways, to see if any works properly?

        var hugedna = Path.Combine(Folders.Base, "dna", "huge.csv");
        var websiteDna = Path.Combine(Folders.Base, "dna", "website.csv");
        var website = (await File.ReadAllLinesAsync(websiteDna)).Select(x => x.Split(',')).ToArray();
        var bytes = website.Select(x => Convert.FromHexString(x[0])).ToArray();
        
        var processed = bytes.Select(x =>
        {
            var count = x[22];
            var parts = 0;
            var maxDataIndex = 0;
            var asd = new HashSet<byte>();
            for (var i = 0; i < 48; i++)
            {
                var start = 24 + i * 4;
                var p1 = x[start];
                var p2 = x[start + 1];
                var c = x[start + 2];
                var empty = x[start + 3];
                
                
                asd.Add(c);
                if (c == 0 || (p1 == 0 && p2 == 0))
                    continue;
                parts++;
                maxDataIndex = i;
            }

            return (count, parts, asd.Count, maxDataIndex);
        }).ToArray();
        
        return;
        
        var huge = (await File.ReadAllLinesAsync(hugedna)).Select(x => x.Split(',')).ToArray();
        var huge_fixed = huge.Select(x => (x[1], Processing.FixWeirdDnaString(x[0]), Processing.FixWeirdDnaString2(x[0])))
            .ToArray();

        var dump = Path.Combine(Folders.Base, "dump");
        Directory.CreateDirectory(dump);
        foreach (var (name, f1, f2) in huge_fixed)
        {
            bool male;
            if (name.Contains("female", StringComparison.OrdinalIgnoreCase))
                male = false;
            else if (name.Contains("male", StringComparison.OrdinalIgnoreCase))
                male = true;
            else
                continue;
            
            const string bDirectory = @"C:\Development\StarCitizenChf\StarCitizenChf\bin\data\localCharacters";

            var default_c = male ? Path.Combine(bDirectory, "default_m", "default_m.chf") : Path.Combine(bDirectory, "default_f", "default_f.chf");

            var chf_f1 = ChfFile.FromChf(default_c);
            var chf_f2 = ChfFile.FromChf(default_c);

            var dnaBytes1 = Convert.FromHexString(f1);
            var dnaBytes2 = Convert.FromHexString(f2);

            //overwrite the dna
            dnaBytes1.CopyTo(chf_f1.Data, 0x48);
            chf_f1.Data[0x46] = 0;
            
            dnaBytes2.CopyTo(chf_f2.Data, 0x48);
            chf_f2.Data[0x46] = 0;

            var sanitized_name = name.Split('\\').Last();
            await chf_f1.WriteToChfFileAsync(Path.Combine(dump, $"{(male ? 'm' : 'f')}_{sanitized_name}_1.chf"));
            await chf_f2.WriteToChfFileAsync(Path.Combine(dump, $"{(male ? 'm' : 'f')}_{sanitized_name}_2.chf"));
        }


        return;
        var web2 = Directory.GetFiles(Folders.WebsiteCharacters, "*.bin", SearchOption.AllDirectories);
        var local2 = Directory.GetFiles(Folders.LocalCharacters, "*.bin", SearchOption.AllDirectories);
        var allBins2 = web2;

        var characters2 = allBins2.Select(x => (name: x, character: StarCitizenCharacter.FromBytes(File.ReadAllBytes(x)))).ToArray();

        var dnas = characters2.Select(p => $"{p.character.Dna.DnaString}, {Path.GetFileNameWithoutExtension(p.name)}").ToArray();

        File.WriteAllLines(Path.Combine(Folders.Base, "dna", "website.csv"), dnas);

        return;
    }
}