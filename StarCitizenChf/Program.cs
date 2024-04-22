using StarCitizenChf;

Decompression.Decompress("data", "decompressed");
return;
var fileToDecompress = Directory.GetFiles( "decompressed", "*.bin", SearchOption.AllDirectories).First();
Decryption.Decrypt(fileToDecompress);

return;
var analysis =  Analysis.AnalyzeSimilarities("decompressed");

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
