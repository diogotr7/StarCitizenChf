using System;
using System.Collections.Generic;
using System.Linq;

namespace StarCitizenChf;

public static class Constants
{
    public static readonly Guid ModelTagM = new("25f439d5-146b-4a61-a999-a486dfb68a49");
    public static readonly Guid ModelTagF = new("d0794a94-efb0-4cad-ad38-2558b4d3c253");

    public static (Guid, string)[] GetAllGuids()
    {
        //reflect over this class and return all guids
        return typeof(Constants).GetFields()
            .Where(f => f.FieldType == typeof(Guid))
            .Select(f => ((Guid)f.GetValue(null), f.Name))
            .ToArray();
    }
}