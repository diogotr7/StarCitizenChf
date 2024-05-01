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
    public ulong NextCount { get; init; }

    public static List<string> DNAData = new();


    private static readonly Guid ManGuid = new("25f439d5-146b-4a61-a999-a486dfb68a49");
    private static readonly Guid WomanGuid = new("d0794a94-efb0-4cad-ad38-2558b4d3c253");

    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var a = GuidUtils.FromBitConverterRepresentation("F5486885A34250FA2D1B1998DC0236BF");
        var b = GuidUtils.FromBitConverterRepresentation("04-41-5F-75-7D-8A-AA-DB-F6-76-1E-FD-58-7B-24-8B");
        var c = GuidUtils.FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82");
        var d = GuidUtils.FromBitConverterRepresentation("E2-04-0B-19-3B-44-48-A4-13-C1-17-62-11-8E-BA-08"); //NO
        var e = GuidUtils.FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82"); //eyelash
        var fromGuid = GuidUtils.FromGuid(Guid.Parse("33e495ca-dec5-4e59-9517-0b8a8535b4d0"));

        var reader = new SpanReader(data);
        reader.Expect<uint>(2); //version?
        reader.Expect<uint>(7); //chf version definitely

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

        var bodyKey = reader.Read<uint>();
        if (bodyKey != BodyProperty.Key)
            throw new Exception();

        var body = BodyProperty.Read(ref reader);
        //this is probably a count of *something*
        if (body.ChildCount != 1)
            throw new Exception();

        var headKey = reader.Read<uint>();
        if (headKey != HeadProperty.Key)
            throw new Exception();

        var head = HeadProperty.Read(ref reader);

        if (head.ChildCount == 0)
            throw new Exception();

        var eyesKey = reader.Read<uint>();
        if (eyesKey != EyesProperty.Key)
            throw new Exception();

        var eyes = EyesProperty.Read(ref reader);

        if (eyes.ChildCount != 0)
            throw new Exception();

        var hairKey = reader.Read<uint>();
        if (hairKey != HairProperty.Key)
            throw new Exception();

        var hair1 = HairProperty.Read(ref reader);

        if (hair1.ChildCount == 1)
        {
            var modifierKey = reader.Read<uint>();
            if (modifierKey != HairModifierProperty.Key)
                throw new Exception();

            var modifier = HairModifierProperty.Read(ref reader);

            //TODO use this for something. Maybe move the modifier read into the HairProperty.Read method?
        }

        EyeBrowProperty? eyebrow = null;
        if (reader.PeekKey == EyeBrowProperty.Key)
        {
            //we peeked above, actually read it now
            _ = reader.Read<uint>();
            eyebrow = EyeBrowProperty.Read(ref reader);
            if (eyebrow.ChildCount != 0)
                throw new Exception();

            //TODO use this for something
        }

        var eyelasheKey = reader.Read<uint>();
        if (eyelasheKey != EyelashProperty.Key)
            throw new Exception();

        var eyelash = EyelashProperty.Read(ref reader);

        //if (eyelash.ChildCount != 0)
        //throw new Exception();

        if (reader.PeekKey == FacialHairProperty.Key)
        {
            _ = reader.Read<uint>();
            var facialHair = FacialHairProperty.Read(ref reader);
            
            if (facialHair.ChildCount > 1) 
                throw new Exception();
            if (facialHair.ChildCount == 1)
            {
                var hairModifierKey = reader.Read<uint>();
                if (hairModifierKey != HairModifierProperty.Key)
                    throw new Exception();
                
                var hairModifier = HairModifierProperty.Read(ref reader);
                //do something with this
            }
        }
        var headMaterialKey = reader.Read<uint>();
        if (headMaterialKey != HeadMaterialProperty.Key)
            throw new Exception();
        
        var headMaterial = HeadMaterialProperty.Read(ref reader);
        
        //unknownprop 6 or 7. i am completely lost here

        return new StarCitizenCharacter()
        {
            Name = fileName,
            Next = reader.Read<uint>().ToString("X8"),
            NextCount = 0
        };

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
            //HairValue = hair?.ChildCount,
            Next = ToBitConverterRepresentation(next),
            Property2 = unk2?.Data,
            Beard = beard?.Id.ToString(),
            BeardValue = beard?.ChildCount,
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

public static class GuidUtils
{
    public static byte[] FromGuid(Guid guid)
    {
        var bytes = guid.ToByteArray();
        var reader = new SpanReader(bytes);

        var a = reader.Read<int>();
        var b = reader.Read<short>();
        var c = reader.Read<short>();
        var d = reader.Read<byte>();
        var e = reader.Read<byte>();
        var f = reader.Read<byte>();
        var g = reader.Read<byte>();
        var h = reader.Read<byte>();
        var i = reader.Read<byte>();
        var j = reader.Read<byte>();
        var k = reader.Read<byte>();

        var result = new byte[16];
        var writer = new SpanWriter(result);

        writer.Write(c);
        writer.Write(b);
        writer.Write(a);
        writer.Write(k);
        writer.Write(j);
        writer.Write(i);
        writer.Write(h);
        writer.Write(g);
        writer.Write(f);
        writer.Write(e);
        writer.Write(d);

        return result;
    }

    public static string ToBitConverterRepresentation(Guid guid)
    {
        return BitConverter.ToString(FromGuid(guid));
    }

    public static Guid FromBitConverterRepresentation(string representation)
    {
        var bytes = representation.Replace("-", "").Chunk(2).Select(x => Convert.ToByte(new string(x), 16)).ToArray();
        var reader = new SpanReader(bytes);
        return reader.ReadGuid();
    }
}