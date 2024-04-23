namespace StarCitizenChf;

public static class Utils
{
    public static (byte[] data, string name)[] LoadFilesWithNames(string path, string pattern = "*")
    {
        return Directory.GetFiles(path, pattern, SearchOption.AllDirectories)
            .Select(x => (File.ReadAllBytes(x), Path.GetFileName(x)))
            .ToArray();
    }
    
    public static string GetSafeDirectoryName(Character character)
    {
        var start = character.title;
        
        Array.ForEach([..Path.GetInvalidPathChars(), ' '], x => start = start.Replace(x, '_'));
        
        return $"{start}-{character.id[..8]}";
    }
}