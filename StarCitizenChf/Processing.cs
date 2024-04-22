namespace StarCitizenChf;

public static class Processing
{
    public static void ConvertToHexView(string inputFolder, string outputFolder, int bytesPerLine)
    {
        var decompressedFiles = Directory.GetFiles(inputFolder, "*.bin", SearchOption.AllDirectories)
            .Select(x => (Path.GetFileName(x), File.ReadAllBytes(x))).ToArray();
        
        Directory.CreateDirectory(outputFolder);
        foreach (var (name, buffer) in decompressedFiles)
        {
            var bufferParts = buffer.Chunk(bytesPerLine);
            var lines = bufferParts.Select(x => string.Join(" ", x.Select(y => y.ToString("X2"))));
            File.WriteAllLines(Path.Combine(outputFolder, Path.ChangeExtension(name, ".txt")), lines);
        }
    }
}