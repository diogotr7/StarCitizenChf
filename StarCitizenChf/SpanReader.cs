using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal ref struct SpanReader(ReadOnlySpan<byte> span)
{
    public ReadOnlySpan<byte> Span { get; } = span;
    public int Position { get; private set; } = 0;
    public ReadOnlySpan<byte> Remaining => Span[Position..];
    
    public uint PeekKey => MemoryMarshal.Read<uint>(Span[Position..]);

    internal T Read<T>() where T : unmanaged
    {
        if (typeof(T) == typeof(Guid))
            throw new InvalidOperationException("Use ReadGuid instead");
        
        var value = MemoryMarshal.Read<T>(Span[Position..]);
        Position += Unsafe.SizeOf<T>();
        return value;
    }
    
    internal ReadOnlySpan<byte> PeekBehind(int bytes, int length)
    {
        return Span.Slice(Position - bytes, length);
    }
    
    public ReadOnlySpan<byte> ReadBytes(int length)
    {
        var value = Span[Position..(Position + length)];
        Position += length;
        return value;
    }
    
    public T Expect<T>(T expected) where T : unmanaged, IEquatable<T>
    {
        if (typeof(T) == typeof(Guid))
            throw new InvalidOperationException("Use ReadGuid instead");
        
        var value = Read<T>();
        if (!value.Equals(expected))
            //throw new InvalidOperationException($"Expected {expected}, got {value} at position 0x{Position - Unsafe.SizeOf<T>():X2}");
            Debugger.Break();
        return value;
    }
    
    public void ExpectBytes(ReadOnlySpan<byte> expected)
    {
        var value = ReadBytes(expected.Length);
        if (!value.SequenceEqual(expected))
            throw new InvalidOperationException($"Expected {BitConverter.ToString(expected.ToArray())}, got {BitConverter.ToString(value.ToArray())} at position 0x{Position - expected.Length:X2}");
    }
    
    
    public void ExpectBytes(string bitConverter)
    {
        var expected = bitConverter.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
        ExpectBytes(expected);
    }

    public Guid ReadGuid()
    {
        //don't ask.
        var a = Read<short>();
        var b = Read<short>();
        var c = Read<int>();
        var d = Read<byte>();
        var e = Read<byte>();
        var f = Read<byte>();
        var g = Read<byte>();
        var h = Read<byte>();
        var i = Read<byte>();
        var j = Read<byte>();
        var k = Read<byte>();
        
        return new Guid(c, b, a, k, j, i, h, g, f, e,d);
    }
}

internal ref struct SpanWriter(Span<byte> span)
{
    public Span<byte> Span { get; } = span;
    public int Position { get; private set; } = 0;
    
    public void Write<T>(T value) where T : unmanaged
    {
        MemoryMarshal.Write(Span[Position..], in value);
        Position += Unsafe.SizeOf<T>();
    }

    public void Write(ReadOnlySpan<byte> span)
    {
        span.CopyTo(Span[Position..]);
        Position += span.Length;
    }

    public void WriteLengthAndString(string value)
    {
        Write((ushort)(value.Length + 1));
        Write(value);
    }
    
    public void Write(string value)
    {
        var byteCount = Encoding.ASCII.GetByteCount(value.AsSpan());
        Encoding.ASCII.GetBytes(value, Span.Slice(Position, byteCount));
        Position += byteCount;
        Write<byte>(0);
    }
}