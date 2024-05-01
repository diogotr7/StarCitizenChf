using System;
using System.Diagnostics;

namespace StarCitizenChf;

//entities/scitem/characters/human.body/body_01_noMagicPocket
internal sealed class BodyProperty
{
    public const uint Key = 0xAB6341AC;
    public const string KeyRep = "AC-41-63-AB";
    
    public static BodyProperty Read(ref SpanReader reader)
    {
        if (reader.ReadGuid() != Guid.Parse("dbaa8a7d-755f-4104-8b24-7b58fd1e76f6"))
            throw new Exception();
        
        var childCount = reader.Read<ulong>();
        if (childCount == 1)
            Debugger.Break();

        return new BodyProperty();
    }
}