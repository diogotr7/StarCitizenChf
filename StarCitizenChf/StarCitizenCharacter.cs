using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable UnusedVariable

namespace StarCitizenChf;

public sealed class StarCitizenCharacter
{
    public string? Name { get; init; }
    public bool? IsMan { get; init; }
    public string? Hair { get; init; }
    public uint? HairValue { get; init; }
    public string? Eyebrow { get; init; }
    public uint? Property2 { get; init; }
    public uint? Property3 { get; init; }
    public string? Beard { get; init; }
    public uint? BeardValue { get; init; }
    public string? Skin { get; init; }

    public ulong Size { get; init; }

    public string Next { get; init; }

    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var reader = new SpanReader(data);
        reader.Expect<uint>(2);//version?
        reader.Expect<uint>(7);//chf version definitely

        var genderSpecificA1 = reader.Read<ulong>();
        var genderSpecificA2 = reader.Read<ulong>();
        
        var isMan = genderSpecificA1 == 0x25F439D5146B4A61 && genderSpecificA2 == 0xA999A486DFB68A49;
        var isWoman = genderSpecificA1 == 0xD0794A94EFB04CAD && genderSpecificA2 == 0xAD382558B4D3C253;
        if (!isMan && !isWoman)
            throw new Exception();
        
        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);

        reader.ExpectBytes("D8-00-00-00");
        reader.ExpectBytes("00-00-00-00");
        reader.ExpectBytes("94-93-D0-FC");

        var genderSpecificB1 = reader.Read<ulong>();

        var isMan2 = genderSpecificB1 == 0x65E740D3_DD6C67F6;
        var isWoman2 = genderSpecificB1 == 0x65D75204_9EF4EB54;
        if (isMan != isMan2 || isWoman != isWoman2)
            throw new InvalidOperationException($"unexpected gender specific data file  {fileName}");
        
        
        reader.Expect<uint>(0);

        //dna head blending data
        var dnaKey = reader.Expect<uint>(0x0004000c);
        var dnaData = reader.ReadBytes(0xC4);

        var someCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.ExpectBytes("AC-41-63-AB");
        reader.ExpectBytes("04-41-5F-75");
        reader.ExpectBytes("7D-8A-AA-DB");
        reader.ExpectBytes("F6-76-1E-FD");
        reader.ExpectBytes("58-7B-24-8B");
        //this is probably a count of *something*
        reader.Expect(1);
        reader.Expect<uint>(0);

        HairProperty? hair = null;
        EyeBrowProperty? eyeBrow = null;
        BeardProperty? beard = null;
        DyeProperty? unk2 = null;
        UnknownProperty3? unk3 = null;
        SkinTextureProperty? skinTextureProperty = null;

        while (true)
        {
            var key = reader.Read<uint>();

            var rep = BitConverter.ToString(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref key, 1)).ToArray());

            switch (rep)
            {
                case UnknownProperty1.KeyRep:
                    UnknownProperty1.Read(ref reader);
                    break;
                case UnknownProperty4.KeyRep:
                    UnknownProperty4.Read(ref reader);
                    break;
                case HairProperty.KeyRep:
                    hair = HairProperty.Read(ref reader);
                    break;
                case EyeBrowProperty.KeyRep:
                    eyeBrow = EyeBrowProperty.Read(ref reader);
                    break;
                case BeardProperty.KeyRep:
                    beard = BeardProperty.Read(ref reader);
                    break;
                case SkinTextureProperty.KeyRep:
                    skinTextureProperty = SkinTextureProperty.Read(ref reader);
                    break;
                case DyeProperty.KeyRep:
                    unk2 = DyeProperty.Read(ref reader);
                    break;
                case UnknownProperty3.KeyRep:
                    unk3 = UnknownProperty3.Read(ref reader);
                    break;
                case UnknownProperty6.KeyRep:
                    UnknownProperty6.Read(ref reader);
                    goto exit;
                case UnknownProperty7.KeyRep:
                    UnknownProperty7.Read(ref reader);
                    goto exit;
                case UnknownProperty8.KeyRep:
                    UnknownProperty8.Read(ref reader);
                    goto exit;
                default:
                    goto exit;
            }
        }

        exit:
        var next = reader.Read<uint>();

        return new StarCitizenCharacter
        {
            Name = fileName,
            IsMan = isMan,
            Hair = BitConverter.ToString(hair.Data[..4].ToArray()),
            HairValue = hair?.ChildCount,
            Eyebrow = eyeBrow is null ? null : BitConverter.ToString(eyeBrow.Data[..4]),
            Next = ToBitConverterRepresentation(next),
            Property2 = unk2?.Data,
            Beard = beard is null ? null : BitConverter.ToString(beard.Data[..4]),
            BeardValue = beard?.Value,
            Property3 = unk3?.Value,
            Skin = skinTextureProperty?.Data is null ? null : BitConverter.ToString(skinTextureProperty.Data[..4]),
            Size = someCount,
        };
    }

    private static string ToBitConverterRepresentation<T>(T value) where T : unmanaged
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
        return BitConverter.ToString(bytes.ToArray());
    }
}