using System;
using System.Collections.Generic;
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
    
    public static List<string> DNAData = new();

    
    private static readonly Guid ManGuid = new("25f439d5-146b-4a61-a999-a486dfb68a49");
    private static readonly Guid WomanGuid = new("d0794a94-efb0-4cad-ad38-2558b4d3c253");
    
    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var stringGuid = "F5486885A34250FA2D1B1998DC0236BF";
        var stringGuidBytes = stringGuid.Replace("-", "").Chunk(2).Select(x => Convert.ToByte(new string(x), 16)).ToArray();
        var aa = new SpanReader(stringGuidBytes);
        var guid = aa.ReadGuid();

        
        var parsedGuidBytes = SpanReader.FromGuid(Guid.Parse("33e495ca-dec5-4e59-9517-0b8a8535b4d0"));
        var parsedGuidString = BitConverter.ToString(parsedGuidBytes);
        
        

        
        
        var reader = new SpanReader(data);
        reader.Expect<uint>(2);//version?
        reader.Expect<uint>(7);//chf version definitely
        var gender = reader.ReadGuid();
        var isMan = gender == ManGuid;
        var isWoman = gender == WomanGuid;
        if (!isMan && !isWoman)
            throw new Exception();
        
        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);
        
        var dnaLength = reader.Read<ulong>();
        if (dnaLength != 216)
            throw new Exception();

        var dnaByteArray = reader.ReadBytes((int)dnaLength);
        var dnaString = string.Join("", dnaByteArray.ToArray().Select(x => x.ToString("X2")));
        
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
        FacialHairProperty? beard = null;
        HairModifierProperty? unk2 = null;
        EyelashProperty? unk3 = null;
        HeadMaterialProperty? skinTextureProperty = null;

        while (true)
        {
            var key = reader.Read<uint>();

            var rep = BitConverter.ToString(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref key, 1)).ToArray());

            switch (rep)
            {
                case BodyProperty.KeyRep:
                    BodyProperty.Read(ref reader);
                    break;
                case HeadProperty.KeyRep:
                    HeadProperty.Read(ref reader);
                    break;
                case EyesProperty.KeyRep:
                    EyesProperty.Read(ref reader);
                    break;
                case HairProperty.KeyRep:
                    hair = HairProperty.Read(ref reader);
                    break;
                case EyeBrowProperty.KeyRep:
                    eyeBrow = EyeBrowProperty.Read(ref reader);
                    break;
                case FacialHairProperty.KeyRep:
                    beard = FacialHairProperty.Read(ref reader);
                    break;
                case HeadMaterialProperty.KeyRep:
                    skinTextureProperty = HeadMaterialProperty.Read(ref reader);
                    break;
                case HairModifierProperty.KeyRep:
                    unk2 = HairModifierProperty.Read(ref reader);
                    break;
                case EyelashProperty.KeyRep:
                    unk3 = EyelashProperty.Read(ref reader);
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
                case UnknownProperty9.KeyRep:
                    UnknownProperty9.Read(ref reader);
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
            Hair = hair?.Id.ToString(),
            HairValue = hair?.ChildCount,
            Eyebrow = eyeBrow?.Id.ToString(),
            Next = ToBitConverterRepresentation(next),
            Property2 = unk2?.Data,
            Beard = beard?.Id.ToString(),
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