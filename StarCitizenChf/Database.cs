using System;
using System.Collections.Generic;
using System.Linq;

namespace StarCitizenChf;

public static class Database
{
    public static Guid UnknownProperty6 = new("");

    public static (Guid, string)[] GetAllGuids()
    {
        //reflect over this class and return all guids
        return typeof(Database).GetFields()
            .Where(f => f.FieldType == typeof(Guid))
            .Select(f => ((Guid)f.GetValue(null), f.Name))
            .ToArray();
    }
}