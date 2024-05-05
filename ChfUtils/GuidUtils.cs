using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChfUtils;

public static class GuidUtils
{
    public static string Shorten(Guid guid) => guid.ToString().Substring(0, 8);

    public static void Test()
    {
        var foundation1 = ToBitConverterRepresentation(Guid.Parse("b5e53e65-bd4a-4f50-bcd1-843ce5fc231b"));
        var foundation2 = ToBitConverterRepresentation(Guid.Parse("318114ee-f184-42f5-86cb-19a321bcb513"));
        var foundation3 = ToBitConverterRepresentation(Guid.Parse("9254513e-8996-4ffb-84f0-7eb6162dddf5"));
        var foundation4 = ToBitConverterRepresentation(Guid.Parse("846d7afe-2725-47ff-a4b0-c6bdb0aaeade"));
        var blemish01 = ToBitConverterRepresentation(Guid.Parse("13cfead6-5662-4dea-995a-5e3f42460e20"));
        var blemish02 = ToBitConverterRepresentation(Guid.Parse("2d8cdf2c-5e5b-482f-ab7c-67e56e2115ae"));
        var headMaterialThing = ToBitConverterRepresentation(Guid.Parse("5d629e70-ff2f-4fc8-829c-b989f5494d4d"));

        var a = FromBitConverterRepresentation("F5486885A34250FA2D1B1998DC0236BF");
        var b = FromBitConverterRepresentation("04-41-5F-75-7D-8A-AA-DB-F6-76-1E-FD-58-7B-24-8B");
        var c = FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82");
        var d = FromBitConverterRepresentation("E2-04-0B-19-3B-44-48-A4-13-C1-17-62-11-8E-BA-08");
        var e = FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82");
        var f = FromBitConverterRepresentation("9A-40-28-05-30-7A-3F-98-E6-F0-65-1A-B8-1E-33-9E");
        var g = FromBitConverterRepresentation("6A-44-54-E7-88-B2-8C-AA-4E-91-D9-7A-10-98-F0-B8");
        var h = FromBitConverterRepresentation("A2-4D-E5-B9-C9-46-9A-22-2C-E5-42-76-00-8F-5E-87");
        var i = FromBitConverterRepresentation("74-42-4E-92-C7-A0-D0-63-B4-D2-A4-4C-5D-76-62-AF");
        var j = FromBitConverterRepresentation("98-4D-E4-F9-95-82-7A-6A-E2-C6-C3-AD-43-74-AA-82");
        var k = FromBitConverterRepresentation("44-4F-BC-21-B3-F3-43-B6-54-79-FD-40-E1-0D-E5-95");
        var z = FromGuid(Guid.Parse("33e495ca-dec5-4e59-9517-0b8a8535b4d0"));
        var z1 = ToBitConverterRepresentation(Guid.Parse("9a66730e-512e-4d21-8ba3-d3ce2c3ebfe6"));
        var z2 = ToBitConverterRepresentation(Guid.Parse("538ab6c3-8bb6-4768-9ad1-cc6387e9c65f"));
        var z3 = ToBitConverterRepresentation(Guid.Parse("e6cb61c7-7740-46b9-9f9c-fd5eb3498e75"));
        var z4 = ToBitConverterRepresentation(Guid.Parse("7e033967-fa65-423e-ba74-af2e810e4cac"));
        var z5 = ToBitConverterRepresentation(Guid.Parse("d9c34b15-40cd-49b1-84bb-a6161bfa5240"));
        var z6 = ToBitConverterRepresentation(Guid.Parse("e76ed31e-9ef4-4fe0-8a46-2c3ed8c6ab1b"));
        var z7 = ToBitConverterRepresentation(Guid.Parse("1d33cab4-50bf-4e7d-8c75-ef56e5e8a1b1"));
        var z8 = ToBitConverterRepresentation(Guid.Parse("2fcd7cc1-a46d-4065-84ba-bfabf9d567ce"));
        var z9 = ToBitConverterRepresentation(Guid.Parse("8a3f884e-4cbf-4c49-a64d-3170e95e54b8"));
        var za = ToBitConverterRepresentation(Guid.Parse("9c55cd1d-b397-4886-b1a4-bc38575916fd"));
        var zb = ToBitConverterRepresentation(Guid.Parse("6a7a8295-f9e4-4d98-82aa-7443adc3c6e2"));
        var zc = ToBitConverterRepresentation(Guid.Parse("003367a7-9873-4a8f-9a27-9b8def193b43"));
        var zd = ToBitConverterRepresentation(Guid.Parse("bc56197f-ec97-43fb-b047-aaf51c8eb3b6"));
        var ze = ToBitConverterRepresentation(Guid.Parse("38219031-5c5a-4d44-9cb1-da8bdc0f2089"));
        var zf = ToBitConverterRepresentation(Guid.Parse("4f79d0fb-389f-48c5-ba3b-9f290b8b4dc2"));
        var teeth = ToBitConverterRepresentation(Guid.Parse("8011c1ea-de55-4a23-9c1b-553ef3017aea"));
        var head_teeth = ToBitConverterRepresentation(Guid.Parse("4a0de194-4b4f-49b8-adb7-1087e9f38941"));
        var asdads = ToBitConverterRepresentation(Guid.Parse("58bff0a5-35d1-420d-a603-691424da5a25"));
    }

    public static string ToBitConverterRepresentation(Guid guid)
    {
        return BitConverter.ToString(FromGuid(guid));
    }

    public static Guid FromBitConverterRepresentation(string representation)
    {
        var bytes = representation.Replace("-", "").Chunk(2).Select(x => Convert.ToByte(new string(x), 16)).ToArray();
        var reader = new SpanReader(bytes);
        return reader.Read<Guid>();
    }

    public static Guid FromBytes(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16)
            throw new ArgumentException("Invalid byte length", nameof(bytes));
        
        var reader = new SpanReader(bytes);
        
        //don't ask.
        var a = reader.Read<short>();
        var b = reader.Read<short>();
        var c = reader.Read<int>();
        var d = reader.Read<byte>();
        var e = reader.Read<byte>();
        var f = reader.Read<byte>();
        var g = reader.Read<byte>();
        var h = reader.Read<byte>();
        var i = reader.Read<byte>();
        var j = reader.Read<byte>();
        var k = reader.Read<byte>();
        
        return new Guid(c, b, a, k, j, i, h, g, f, e,d);
    }
    
    public static byte[] FromGuid(Guid guid)
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref guid, 1));
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
}