using System;
using System.Linq;

namespace StarCitizenChf;

public static class GuidUtils
{
    public static void Test()
    {
        var a = FromBitConverterRepresentation("F5486885A34250FA2D1B1998DC0236BF");
        var b = FromBitConverterRepresentation("04-41-5F-75-7D-8A-AA-DB-F6-76-1E-FD-58-7B-24-8B");
        var c = FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82");
        var d = FromBitConverterRepresentation("E2-04-0B-19-3B-44-48-A4-13-C1-17-62-11-8E-BA-08");
        var e = FromBitConverterRepresentation("3B-44-48-A4-13-C1-17-62-11-8E-BA-08-B1-1B-AA-82");
        var f = FromBitConverterRepresentation("9A-40-28-05-30-7A-3F-98-E6-F0-65-1A-B8-1E-33-9E");
        var g = FromBitConverterRepresentation("6A-44-54-E7-88-B2-8C-AA-4E-91-D9-7A-10-98-F0-B8");
        var z = FromGuid(Guid.Parse("33e495ca-dec5-4e59-9517-0b8a8535b4d0"));
    }

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