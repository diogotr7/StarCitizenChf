using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class FacialHairProperty
{
    public const uint Key = 0x98EFBB1C;
    public const string KeyRep = "1C-BB-EF-98";

    public Guid Id { get; set; }
    public uint ChildCount { get; set; }

    public static FacialHairProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var count = reader.Read<uint>();
        switch (count)
        {
            case 0:
                reader.Expect(6);
                reader.Expect(5);
                return new FacialHairProperty() { Id = guid, ChildCount = 0 };
            case 1:
                reader.Expect<uint>(0);
                return new FacialHairProperty() { Id = guid, ChildCount = 1 };
            default:
                Debugger.Break();
                throw new Exception();
        }
    }
}