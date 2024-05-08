using System.Runtime.InteropServices;

namespace ChfParser;

public static class GuidUtils
{
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