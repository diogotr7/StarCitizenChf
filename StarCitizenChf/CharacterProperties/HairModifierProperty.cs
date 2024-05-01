﻿using System;
using System.Diagnostics;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/appearance_modifier/hair_variant/hair_var_brown.xml
internal sealed class HairModifierProperty
{
    public const uint Key = 0x_e7_80_9d_46;
    public const string KeyRep = "46-9D-80-E7";
    
    public Guid Id { get; set; }
    public ulong ChildCount { get; set; }
    
    public static HairModifierProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
        reader.Expect(0);
        var count = reader.Read<uint>();//usually 0 sometimes 6

        switch (count)
        {
            case 0:
                return new HairModifierProperty() { Id = guid, ChildCount = count };
            case 6:
                //the data i have has this 5 here but the next property is right after.
                //Unknown what this is.
                //count of how many objects are left to read??
                reader.Expect(5);
                return new HairModifierProperty() { Id = guid, ChildCount = count };
            default:
                Debugger.Break();
                return new HairModifierProperty() { Id = guid, ChildCount = count };
        }
    }
}