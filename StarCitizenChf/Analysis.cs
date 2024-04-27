using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;

namespace StarCitizenChf;

public record AnalysisResult(List<(int,int)> CommonSequences, int[] CommonBytes, byte[] ValuesAtCommonBytes);

public static class Analysis
{
    public static AnalysisResult AnalyzeSimilarities(IEnumerable<string> paths)
    {
        var decompressedFiles = paths.Select(File.ReadAllBytes).ToArray();

        var smallest = decompressedFiles.MinBy(x => x.Length)!.Length;
        //analyze byte by byte if it is the same in all files
        var commonBytes = new List<int>();
        for (var i = 0; i < smallest; i++)
        {
            if (decompressedFiles.All(b => b[i] == decompressedFiles[0][i]))
                commonBytes.Add(i);
        }

        var valuesAtCommonBytes = commonBytes.Select(i => decompressedFiles[0][i]).ToArray();

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

    public static bool IsMale(ReadOnlySpan<byte> decompressedData)
    {
        ReadOnlySpan<byte> woman = [0xAD, 0x4C, 0xB0, 0xEF, 0x94, 0x4A, 0x79, 0xD0, 0x53, 0xC2, 0xD3, 0xB4, 0x58, 0x25, 0x38, 0xAD];
        ReadOnlySpan<byte> man = [0x61, 0x4A, 0x6B, 0x14, 0xD5, 0x39, 0xF4, 0x25, 0x49, 0x8A, 0xB6, 0xDF, 0x86, 0xA4, 0x99, 0xA9];
        
        var search = decompressedData.Slice(8, 16);
        
        if (search.SequenceEqual(woman))
            return false;
        if (search.SequenceEqual(man))
            return true;
        
        throw new Exception();
    }
    
    public static async Task PrintAllToFileAsync(IEnumerable<string> files, string output)
    {
        var lines = new List<string>();
        foreach (var r in files)
        {
            var bytes = await File.ReadAllBytesAsync(r);
            var chunks = bytes.Chunk(4).ToArray();
            var stringChunks = chunks.Select(c => BitConverter.ToString(c)).ToArray();
            var merged = string.Join("|", stringChunks);
            lines.Add($"{merged}: {Path.GetFileName(r)}");
        }
        await File.WriteAllLinesAsync(output, lines);
    }
    
    public static void BruteForceCommonBytes(IEnumerable<string> files)
    {
        var decompressedFiles = files.Select(File.ReadAllBytes).ToArray();
        var firstFile = decompressedFiles[0];
        var chunks = firstFile.Chunk(4).Where(c => c.Any(i => i != 0));
        var commonString = new HashSet<string>();
        
        foreach (var chunk in chunks)
        {
            if (decompressedFiles.All(f => f.AsSpan().IndexOf(chunk) != -1))
            {
                commonString.Add(BitConverter.ToString(chunk));
            }
        }

        Console.WriteLine($"Found {commonString.Count} common chunks");
        foreach (var chunk in commonString)
        {
            Console.WriteLine(chunk);
        }
    }
}