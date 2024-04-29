using System;
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
    
    public uint Peek => MemoryMarshal.Read<uint>(Span[Position..]);

    internal T Read<T>() where T : unmanaged
    {
        var value = MemoryMarshal.Read<T>(Span[Position..]);
        Position += Unsafe.SizeOf<T>();
        return value;
    }
    
    public ReadOnlySpan<byte> ReadBytes(int length)
    {
        var value = Span[Position..(Position + length)];
        Position += length;
        return value;
    }

    public string ReadLengthAndString()
    {
        int length = Read<ushort>();
        
        if (length == 0)
            return string.Empty;
        
        return Encoding.ASCII.GetString(ReadBytes(length)[..^1]);
    }
    
    public T Expect<T>(T expected) where T : unmanaged, IEquatable<T>
    {
        var value = Read<T>();
        if (!value.Equals(expected))
            throw new InvalidOperationException($"Expected {expected}, got {value} at position 0x{Position - Unsafe.SizeOf<T>():X2}");
        return value;
    }
    
    public void ExpectBytes(ReadOnlySpan<byte> expected)
    {
        var value = ReadBytes(expected.Length);
        if (!value.SequenceEqual(expected))
            throw new InvalidOperationException($"Expected {BitConverter.ToString(expected.ToArray())}, got {BitConverter.ToString(value.ToArray())} at position 0x{Position - expected.Length:X2}");
    }
    
    //AC-41-63-AB
    public void ExpectBytes(string bitConverter)
    {
        var expected = bitConverter.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
        ExpectBytes(expected);
    }
}