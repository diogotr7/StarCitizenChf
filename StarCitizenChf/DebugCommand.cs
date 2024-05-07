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
    public ValueTask ExecuteAsync(IConsole console)
    {
        const string weirdDna =
            "000CC3D0000CC3D0000DB0A0000DB0A00008C1740005B19E0005B19E00070000000700000004000000010000000A000000053C2E00053C2E000200000002000000060000000C0000000C00000009AA1C0009AA1C00010000000D000000080000000300000003000000080000000800000002000000090000000900000008000000080000000B4B8600044BFE00054D090009000000090000000E4F5E000E4F5E00053E8A00024E6000024E60000055E2000055E20008B478000BB400000CB2F5";
        var xxx = Processing.FixWeirdDnaString(weirdDna);

        // var hugedna = Path.Combine(Folders.Base, "dna", "huge.csv");
        // var hugednaF = Path.Combine(Folders.Base, "dna", "huge_fixed.csv");
        // var huge = File.ReadAllLines(hugedna).Select(x => x.Split(',')).ToArray();
        // var huge_fixed = huge.Select(x => $"{Processing.FixWeirdDnaString(x[0])}, {x[1]}").ToArray();
        // File.WriteAllLines(hugednaF, huge_fixed);

        var web2 = Directory.GetFiles(Folders.WebsiteCharacters, "*.bin", SearchOption.AllDirectories);
        var local2 = Directory.GetFiles(Folders.LocalCharacters, "*.bin", SearchOption.AllDirectories);
        var allBins2 = web2.Concat(local2).ToArray();

        var characters2 = allBins2.Select(x =>  StarCitizenCharacter.FromBytes(File.ReadAllBytes(x))).ToArray();

        var viz = characters2.Select(c => new
        {
            dna1 = c.Dna.DnaString.Substring(16, 8),
            dna2 = c.Dna.DnaString.Substring(44, 4),
            body = c.BodyType.Type,
            headmaterial = c.HeadMaterial.Material
        }).ToArray();

        return default;
    }
}