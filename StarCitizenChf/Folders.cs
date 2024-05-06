using System.IO;

namespace StarCitizenChf;

public class Folders
{
    public Folders(string baseFolder)
    {
        if (!Directory.Exists(baseFolder))
            throw new DirectoryNotFoundException($"Directory {baseFolder} not found");
        
        Base = Path.Combine(baseFolder, "data");
        WebsiteCharacters = Path.Combine(Base, "websiteCharacters"); Directory.CreateDirectory(WebsiteCharacters);
        LocalCharacters = Path.Combine(Base, "localCharacters"); Directory.CreateDirectory(LocalCharacters);
        ModdedCharacters = Path.Combine(Base, "moddedCharacters"); Directory.CreateDirectory(ModdedCharacters);
        ColorsFolder = Path.Combine(Base, "colors"); Directory.CreateDirectory(ColorsFolder);
        MetadataFile = Path.Combine(Base, "metadata.json");
    }
    
    public string Base { get; }
    public string WebsiteCharacters { get; }
    public string LocalCharacters { get; }
    public string ModdedCharacters { get; }
    public string ColorsFolder { get; }
    public string MetadataFile { get; }
}