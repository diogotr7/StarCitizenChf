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

    public static void ImportGameCharacters(string outputFolder)
    {
        var inputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Roberts Space Industries","StarCitizen","EPTU","user","client","0","CustomCharacters");

        var characters = Directory.GetFiles(inputFolder, "*.chf", SearchOption.AllDirectories);
        
        foreach (var character in characters)
        {
            var name = Path.GetFileNameWithoutExtension(character);
            var output = Path.Combine(outputFolder, name);
            
            if (Directory.Exists(output))
                continue;
            
            Directory.CreateDirectory(output);
            File.Copy(character, Path.Combine(output, $"{name}.chf"));
        }
    }
}