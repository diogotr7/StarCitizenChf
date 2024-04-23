namespace StarCitizenChf;

public static class Processing
{
    public static Task ConvertToHexView(string inputFilename, string outputFilename, int bytesPerLine = 16)
    {
        if (!inputFilename.EndsWith(".bin")) 
            throw new ArgumentException("Input file must be a .bin file", nameof(inputFilename));
        if (!outputFilename.EndsWith(".txt"))
            throw new ArgumentException("Output file must be a .txt file", nameof(outputFilename));
        
        var buffer = File.ReadAllBytes(inputFilename);
        var bufferParts = buffer.Chunk(bytesPerLine);
        var lines = bufferParts.Select(x => string.Join(" ", x.Select(y => y.ToString("X2"))));
        return File.WriteAllLinesAsync(outputFilename, lines);
    }
}