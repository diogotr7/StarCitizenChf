using StarCitizenChf;

var csprojFolder = Path.GetFullPath(@"..\..\..\");
var original = Path.Combine(csprojFolder, "original");
var decompressed = Path.Combine(csprojFolder, "decompressed");
var hex = Path.Combine(csprojFolder, "hex");

var decompFiles = Load(decompressed);
foreach (var (data, name) in decompFiles)
{
    try
    {
        var male =     Analysis.IsMale(data);
        Console.WriteLine($"{name} is {(male ? "male" : "female")}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"{name} is unknown");
    }
}

return;

var fileToDecompress = Directory.GetFiles( decompressed, "*.bin", SearchOption.AllDirectories).First();
Decryption.Decrypt(fileToDecompress);

var analysis =  Analysis.AnalyzeSimilarities(decompressed);

//compute markdown table to visualize the data
const string markdown = "| Index | Value |";
const string separate = "|-------|-------|";
var lines = new List<string> { markdown, separate };
for (var i = 0; i < analysis.CommonBytes.Length; i++)
{
    var continuous = i > 0 && analysis.CommonBytes[i] == analysis.CommonBytes[i - 1] + 1;

    if (!continuous)
    {
        lines.Add(separate);
    }
    
    lines.Add($"| 0x{analysis.CommonBytes[i]:X3} | 0x{analysis.ValuesAtCommonBytes[i]:X2}  |");
}
Console.WriteLine(string.Join(Environment.NewLine, lines));


static (byte[] data, string name)[] Load(string path)
{
    return Directory.GetFiles(path, "*", SearchOption.AllDirectories)
        .Select(x => (File.ReadAllBytes(x), Path.GetFileName(x)))
        .ToArray();
}