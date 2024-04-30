using System;
using System.Diagnostics;

namespace StarCitizenChf;

internal sealed class BeardProperty
{
    public const uint Key = 0x98EFBB1C;
    public const string KeyRep = "1C-BB-EF-98";

    public byte[] Data { get; set; }
    public uint Value { get; set; }

    public static BeardProperty Read(ref SpanReader reader)
    {
        var unknownData = reader.ReadBytes(sizeof(uint) * 4);
        var count = reader.Read<uint>(); //usually zero sometimes 1
        switch (count)
        {
            case 0:
                reader.Expect(6);
                reader.Expect(5);
                return new BeardProperty() { Data = unknownData.ToArray(), Value = 0 };
            case 1:
                reader.Expect<uint>(0);
                Console.WriteLine($"Beard next: {reader.Peek:X8}");
                return new BeardProperty() { Data = unknownData.ToArray(), Value = 1 };
            default:
                Debugger.Break();
                throw new Exception();
        }
    }
}