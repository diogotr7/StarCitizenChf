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
        const string fucky = "000000000000000000060000000600000006C77000000000000000000009000000090000000C5EC9000A0000000D000000096378000963780005B8010005B80100030000000D0000000D00000005BAB10005BAB100090000000147E2000900000003000000030000000147FD000147FD0001388E000EBA50000EBA50000E0000000E0000000300000004B81C00046585000E9C86000E9C86000B0000000B000000080000000C45AE000C45AE0007454D0007454D0001A13500070000000E9A79";

        var f1 = Processing.FixWeirdDnaString(fucky);
        var f2 = Processing.FixWeirdDnaString2(fucky);
        
        
            await FigureOutHeadCount();

        await fixhuge();

        //ExtractWebsiteDnas();
    }

    private static async Task FigureOutHeadCount()
    {
        var dnaData = Path.Combine(Folders.Base, "dna", "website.csv");
        var lines = (await File.ReadAllLinesAsync(dnaData)).Select(x => x.Split(',')).ToArray();
        var bytes = lines.Select(x => Convert.FromHexString(x[0])).Distinct().ToArray();
        
        var processed = bytes.Select(x =>
        {
            if (x.Length != 216)
                throw new Exception("length not 192");
            
            var count = x[0x16];
            var nonZeroParts = 0;
            var maxDataIndex = 0;
            var diffHeads = new HashSet<byte>();
            for (var i = 0; i < 48; i++)
            {
                var start = 0x18 + i * 4;
                var val1 = x[start];
                var val2 = x[start + 1];
                var headId = x[start + 2];
                
                if (x[start + 3] != 0)
                    throw new Exception("empty not 0");
                
                diffHeads.Add(headId);
                
                if (headId == 0 || (val1 == 0 && val2 == 0))
                    continue;
                
                nonZeroParts++;
                maxDataIndex = i;
            }

            return new
            {
                count,
                nonZeroParts,
                maxDataIndex,
                diffHeadCount = diffHeads.Count
            };
        }).ToArray();
        
        return;
    }

    private static void ExtractWebsiteDnas()
    {
        var websiteCharacters = Directory.GetFiles(Folders.WebsiteCharacters, "*.bin", SearchOption.AllDirectories);
        var characters2 = websiteCharacters.Select(x => (name: x, character: StarCitizenCharacter.FromBytes(File.ReadAllBytes(x)))).ToArray();
        var dnas = characters2.Select(p => $"{p.character.Dna.DnaString}, {Path.GetFileNameWithoutExtension(p.name)}").ToArray();

        File.WriteAllLines(Path.Combine(Folders.Base, "dna", "website.csv"), dnas);
    }

    private static async Task fixhuge()
    {
        var hugeData = Path.Combine(Folders.Base, "dna", "huge.csv");
        var huge = (await File.ReadAllLinesAsync(hugeData)).Select(x => x.Split(',')).ToArray();
        var huge_fixed = huge
            .Where(x => x[0].Length == 384)
            .Select(x => (x[1], Processing.FixWeirdDnaString(x[0]), Processing.FixWeirdDnaString2(x[0])))
            .ToArray();
        
        // var huge_fixed_csv = huge_fixed.DistinctBy(x => $"{x.Item2},{x.Item3}").Select(x => $"{x.Item2},{x.Item3},{x.Item1}");
        // await File.WriteAllLinesAsync(Path.Combine(Folders.Base, "dna", "huge_fixed.csv"), huge_fixed_csv);

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
            const uint dnaStart = 0x30;//0x9493

            dnaBytes1.CopyTo(chf_f1.Data, dnaStart + 0x18);
            chf_f1.Data[dnaStart + 0x16] = 29;
            
            dnaBytes2.CopyTo(chf_f2.Data, dnaStart + 0x18);
            chf_f2.Data[dnaStart + 0x16] = 29;

            var sanitized_name = name.Split('\\').Last();
            await chf_f1.WriteToChfFileAsync(Path.Combine(dump, $"{(male ? 'm' : 'f')}_{sanitized_name}_1.chf"));
            await chf_f2.WriteToChfFileAsync(Path.Combine(dump, $"{(male ? 'm' : 'f')}_{sanitized_name}_2.chf"));
        }
    }
}