namespace StarCitizenChf;

public record AnalysisResult(List<(int,int)> CommonSequences, int[] CommonBytes, byte[] ValuesAtCommonBytes);

public static class Analysis
{
    public static AnalysisResult AnalyzeSimilarities(string inputFolder)
    {
        var decompressedFiles = Directory.GetFiles(inputFolder, "*.bin", SearchOption.AllDirectories)
            .Select(x => (Path.GetFileName(x), File.ReadAllBytes(x))).ToArray();

        var smallest = decompressedFiles.MinBy(x => x.Item2.Length).Item2.Length;
        //analyze byte by byte if it is the same in all files
        var commonBytes = new List<int>();
        for (var i = 0; i < smallest; i++)
        {
            if (decompressedFiles.All(b => b.Item2[i] == decompressedFiles[0].Item2[i]))
                commonBytes.Add(i);
        }

        var valuesAtCommonBytes = commonBytes.Select(i => decompressedFiles[0].Item2[i]).ToArray();

        //compute sequences of bytes in a row. This is useful for finding patterns in the data
        //input: 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15
        //output: 0-3, 5-10, 12-15
        var sequences = new List<(int, int)>();
        for (var i = 0; i < commonBytes.Count; i++)
        {
            var start = commonBytes[i];
            var end = start;
            while (i + 1 < commonBytes.Count && commonBytes[i + 1] == end + 1)
            {
                end++;
                i++;
            }

            sequences.Add((start, end));
        }

        return new AnalysisResult(sequences, commonBytes.ToArray(), valuesAtCommonBytes);
    }
}