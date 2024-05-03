using System;
using System.Diagnostics;
using ChfUtils;

namespace ChfParser;

public sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";

    public Guid Id { get; set; }

    private static List<string> stupid = new();
    private static List<int> stupid2 = new();

    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var additionalParams = reader.Read<uint>();

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);
        var hmmm = BitConverter.ToString(reader.ReadBytes(4).ToArray()); //reader.ExpectBytes("8E-9E-12-72");//or "05-8A-37-A5"
        var important = reader.Read<uint>();

        //i = 0
        reader.Expect<uint>(0);
        reader.Expect<uint>(4);
        //is this just a null guid?
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<byte>(0);

        //i = 1
        reader.Expect<uint>(0);
        reader.Expect<uint>(9);
        //is this just a null guid?
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<byte>(0);
        
        //i = 2+
        for (var i = 2; i < important; i++)
        {
            //21 = sizeof(uint) + sizeof(byte) + sizeof(Guid)
            var mysteryBytes = BitConverter.ToString(reader.PeekBytes(21).ToArray());

            reader.Expect<uint>(0);
            var mysteryCount = reader.Read<byte>(); //probably wrong
            var mysteryGuid = reader.Read<Guid>();
            //Makeup Guid ^^ (lips, eyes, etc)
            //could be makeup material things?
            //count has been seen to be 12 and 14 so far.

            Console.WriteLine($"HeadMaterialProperty: {mysteryCount} {mysteryGuid} ({i + 1}/{important})");
        }

        //TestParser.Read(ref reader);

        return new HeadMaterialProperty() { Id = guid };
    }
}