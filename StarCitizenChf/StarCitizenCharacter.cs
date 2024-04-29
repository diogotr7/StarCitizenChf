using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable UnusedVariable

namespace StarCitizenChf;

public sealed class StarCitizenCharacter
{
    public required string Name { get; init; }
    public required bool IsMan { get; init; }
    public required uint Count_x140 { get; init; }
    public required string Haircut { get; init; }
    public required uint Count_x178 { get; init; }
    public required string x180 { get; init; }
    public string? EyeBrow { get; init; }
    
    public string Next { get; init; }
    public string Key3Last { get; init; }

    private static ReadOnlySpan<byte> GenderSpecificA_F =>
        [0xAD, 0x4C, 0xB0, 0xEF, 0x94, 0x4A, 0x79, 0xD0, 0x53, 0xC2, 0xD3, 0xB4, 0x58, 0x25, 0x38, 0xAD];

    private static ReadOnlySpan<byte> GenderSpecificA_M =>
        [0x61, 0x4A, 0x6B, 0x14, 0xD5, 0x39, 0xF4, 0x25, 0x49, 0x8A, 0xB6, 0xDF, 0x86, 0xA4, 0x99, 0xA9];

    private static ReadOnlySpan<byte> GenderSpecificB_F => [0x54, 0xEB, 0xF4, 0x9E, 0x04, 0x52, 0xD7, 0x65];

    private static ReadOnlySpan<byte> GenderSpecificB_M => [0xF6, 0x67, 0x6C, 0xDD, 0xD3, 0x40, 0xE7, 0x65];

    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var reader = new SpanReader(data);
        reader.Expect<uint>(2);
        reader.Expect<uint>(7);
        var genderSpecificA = reader.ReadBytes(16);
        var isMan = genderSpecificA.SequenceEqual(GenderSpecificA_M);
        var isWoman = genderSpecificA.SequenceEqual(GenderSpecificA_F);

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);

        var key1 = reader.Expect<uint>(0x000000d8); //idk
        reader.Expect<uint>(0x00000000);
        reader.Expect<uint>(0xfcd09394); //some key for gender-specific
        var genderSpecificB = reader.ReadBytes(8);
        var isMan2 = genderSpecificB.SequenceEqual(GenderSpecificB_M);
        var isWoman2 = genderSpecificB.SequenceEqual(GenderSpecificB_F);
        if (isMan != isMan2 || isWoman != isWoman2)
            throw new InvalidOperationException($"unexpected gender specific data file  {fileName}");

        reader.Expect<uint>(0);

        //dna head blending data
        var dnaKey = reader.Expect<uint>(0x0004000c);
        var dnaData = reader.ReadBytes(0xcc);

        Debug.Assert(reader.Position == 0x110);

        //no clue at all here
        reader.ExpectBytes("AC-41-63-AB");
        reader.ExpectBytes("04-41-5F-75");
        reader.ExpectBytes("7D-8A-AA-DB");
        reader.ExpectBytes("F6-76-1E-FD");
        reader.ExpectBytes("58-7B-24-8B");
        reader.ExpectBytes("01-00-00-00");
        reader.Expect<uint>(0);
        reader.ExpectBytes("B9-0D-01-47");
        reader.ExpectBytes("50-45-80-BF");
        reader.ExpectBytes("B3-FA-5C-1D");
        reader.ExpectBytes("6E-08-A7-96");
        reader.ExpectBytes("E8-39-AB-B4");
        //here be dragons

        if (reader.Position != 0x140)
            throw new InvalidOperationException("unexpected data length");

        var x140 = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.ExpectBytes("50-55-BB-C5");
        reader.ExpectBytes("71-48-60-E1");
        reader.ExpectBytes("63-A3-4C-6B");
        reader.ExpectBytes("10-03-B4-67");
        reader.ExpectBytes("74-E4-09-B7");
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("95-1A-60-13");
        var haircut = reader.ReadBytes(16);

        var x178 = reader.Read<uint>(); //zero or one
        reader.Expect<uint>(0);

        const uint possibleKey1 = 0x_17_87_EE_22;//most "normal" characters have this
        const uint possibleKey2 = 0x_e7_80_9d_46;//only one weird dude, what the hell is this?
        const uint possibleKey3 = 0x_19_0b_04_e2;//none of these characters have eyebrows
        var x180_key = reader.Read<uint>();

        byte[]? eyeBrow = null;
        string? key3last = null;
        if (x180_key == possibleKey1)
        {
            eyeBrow = reader.ReadBytes(sizeof(uint) * 4).ToArray();
            reader.Expect<uint>(0);
            reader.Expect<uint>(0);
        }
        else if (x180_key == possibleKey2)
        {
            reader.ExpectBytes("AB-4D-9A-E4");
            reader.ExpectBytes("E5-4C-CE-12");
            reader.ExpectBytes("F2-DD-AA-2F");
            reader.ExpectBytes("26-AD-31-9D");
            reader.Expect<uint>(0);
            var count = reader.Read<uint>();//usually 0 sometimes 6
        }
        else if (x180_key == possibleKey3)
        {
            reader.ExpectBytes("3B-44-48-A4");
            reader.ExpectBytes("13-C1-17-62");
            reader.ExpectBytes("11-8E-BA-08");
            reader.ExpectBytes("B1-1B-AA-82");
            reader.Expect<uint>(0);
            var count3 = reader.Read<uint>();//0, 5, 6
            
            key3last = ToBitConverterRepresentation(count3);
            
            //if count3 == 0, we can read the next key. If not, we prob have more data?
            //sometimes the next piece of data here is a 5, sometimes it's possibleKey4. hwhat?
        }
        else
        {
            throw new InvalidOperationException($"unexpected key {x180_key:X8}");
        }

        var next = reader.Read<uint>();

        const uint possibleKey4 = 0x_98_EF_BB_1C;//usually only appears at the end of possibleKey3

        return new StarCitizenCharacter
        {
            Name = fileName,
            IsMan = isMan,
            Count_x140 = x140,
            Haircut = BitConverter.ToString(haircut[..4].ToArray()),
            Count_x178 = x178,
            x180 = ToBitConverterRepresentation(x180_key),
            EyeBrow = eyeBrow is null ? null : BitConverter.ToString(eyeBrow[..4]),
            Next = ToBitConverterRepresentation(next),
            Key3Last = key3last ?? string.Empty
        };
    }

    private static string ToBitConverterRepresentation<T>(T value) where T : unmanaged
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
        return BitConverter.ToString(bytes.ToArray());
    }
}