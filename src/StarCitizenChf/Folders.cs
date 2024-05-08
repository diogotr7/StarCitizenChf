using System;
using System.IO;

namespace StarCitizenChf;

public static class Folders
{
    public static readonly string Base = Path.Combine(Environment.CurrentDirectory, "data");
    public static readonly string WebsiteCharacters = Path.Combine(Base, "websiteCharacters");
    public static readonly string LocalCharacters  = Path.Combine(Base, "localCharacters");
    public static readonly string ModdedCharacters  = Path.Combine(Base, "moddedCharacters");
    public static readonly string StarCitizenCharactersFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Roberts Space Industries", "StarCitizen", "EPTU", "user", "client", "0", "CustomCharacters");
}