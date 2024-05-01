﻿using System;
using ChfUtils;

namespace ChfParser;

internal sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";
    
    public Guid Id { get; set; }
    
    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
        var alsoGuidMaybe = reader.Read<uint>();
        
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(1);
        reader.Expect(5);

        return new HeadMaterialProperty() { Id = guid };
    }
}