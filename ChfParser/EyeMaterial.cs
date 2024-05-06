﻿using ChfUtils;

namespace ChfParser;

public sealed class EyeMaterial
{
    public const uint Key = 0xA047885E;
    
    public required Color EyeColor { get; init; }
    
    public static EyeMaterial Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        reader.Expect(Guid.Empty);
        reader.Expect(0xCE9DF055);
        reader.Expect(Guid.Empty);
        reader.Expect(1);
        reader.Expect(5);
        reader.Expect(0x9736C44B);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        var colorBlock = ColorBlock2.Read(ref reader);
        reader.Expect<uint>(5);
        
        return new EyeMaterial
        {
            EyeColor = colorBlock.Color01 ?? throw new Exception("Expected eye color")
        };
    }
}