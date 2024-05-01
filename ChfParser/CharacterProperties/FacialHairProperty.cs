using System;
using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

internal sealed class FacialHairProperty
{
    public const uint Key = 0x98EFBB1C;
    public const string KeyRep = "1C-BB-EF-98";

    public Guid Id { get; set; }
    public HairModifierProperty? Modifier { get; init; }

    private static FacialHairProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
        var count = reader.Read<uint>();
        switch (count)
        {
            case 0:
                reader.Expect(6);
                reader.Expect(5);
                return new FacialHairProperty() { Id = guid };
            case 1:
                reader.Expect<uint>(0);
                
                var hairModifier = HairModifierProperty.Read(ref reader);
                Console.WriteLine($"facialhairmodifier count: {hairModifier.ChildCount}");

                return new FacialHairProperty() { Id = guid, Modifier = hairModifier};
            default:
                Debugger.Break();
                throw new Exception();
        }
    }
            
    public static FacialHairProperty? ReadOptional(ref SpanReader reader)
    {
        if (reader.PeekKey != Key)
            return null;
            
        return Read(ref reader);
    }
}