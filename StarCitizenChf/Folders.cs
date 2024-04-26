namespace StarCitizenChf;

public class Folders
{
    public Folders(string baseFolder)
    {
        if (!Directory.Exists(baseFolder))
            throw new DirectoryNotFoundException($"Directory {baseFolder} not found");
        
        Base = Path.Combine(baseFolder, "data");
        WebsiteCharacters = Path.Combine(Base, "websiteCharacters");
        LocalCharacters = Path.Combine(Base, "localCharacters");
        ModdedCharacters = Path.Combine(Base, "moddedCharacters");
        Temp = Path.Combine(Base, "temp");
        
        foreach (var folder in new[] {WebsiteCharacters, LocalCharacters, ModdedCharacters, Temp})
            Directory.CreateDirectory(folder);
        
        Directory.EnumerateFiles(Temp).ToList().ForEach(File.Delete);
        
        MetadataFile = Path.Combine(Base, "metadata.json");
    }
    
    public string Base { get; }
    public string WebsiteCharacters { get; }
    public string LocalCharacters { get; }
    public string ModdedCharacters { get; }
    public string Temp { get; }
    public string MetadataFile { get; }
}