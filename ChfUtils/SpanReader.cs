using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChfUtils;

public ref struct SpanReader(ReadOnlySpan<byte> span)
{
    public ReadOnlySpan<byte> Span { get; } = span;
    public int Position { get; private set; } = 0;
    
    public ReadOnlySpan<byte> Remaining => Span[Position..];
    
    public string NextKey => BitConverter.ToString(Span[Position..(Position + 4)].ToArray());
    public uint NextUint=> Peek<uint>();
    public Guid NextGuid => Peek<Guid>();
    public float NextFloat => Peek<float>();
    public ulong NextUlong => Peek<ulong>();

    /// <summary>
    ///     Reads a value from the span and advances the position.
    /// </summary>
    public T Read<T>() where T : unmanaged
    {
        var value = Peek<T>();
        Position += Unsafe.SizeOf<T>();
        return value;
    }
    
    /// <summary>
    ///     Reads a value from the span without advancing the position.
    /// </summary>
    public T Peek<T>() where T : unmanaged
    {
        //special case for Guids, since they're stored in a weird way
        if (typeof(T) == typeof(Guid))
        {
            var guid = GuidUtils.FromBytes(Span[Position..(Position + 16)]);
            return Unsafe.As<Guid, T>(ref guid);
        }
        
        return MemoryMarshal.Read<T>(Span[Position..]);
    }
    
    /// <summary>
    ///     Reads a number of bytes from the span and advances the position.
    /// </summary>
    public ReadOnlySpan<byte> ReadBytes(int length)
    {
        var value = Span[Position..(Position + length)];
        Position += length;
        return value;
    }
    
    /// <summary>
    ///     Reads a T value from the span and checks if it matches the expected value.
    /// </summary>
    public T Expect<T>(T expected) where T : unmanaged, IEquatable<T>
    {
        var value = Read<T>();
        
        if (!value.Equals(expected))
            throw new InvalidOperationException($"Expected {expected}, got {value} at position 0x{Position - Unsafe.SizeOf<T>():X2}");
        
        return value;
    }

    /// <summary>
    ///     Reads a number of bytes from the span and checks if it matches the expected value.
    /// </summary>
    /// <param name="expected"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void ExpectBytes(ReadOnlySpan<byte> expected)
    {
        var value = ReadBytes(expected.Length);
        
        if (!value.SequenceEqual(expected))
            throw new InvalidOperationException($"Expected {BitConverter.ToString(expected.ToArray())}, got {BitConverter.ToString(value.ToArray())} at position 0x{Position - expected.Length:X2}");
    }
    
    /// <summary>
    ///     Reads a number of bytes from the span and checks if it matches the expected value.
    /// </summary>
    /// <param name="bitConverter"></param>
    public void ExpectBytes(string bitConverter)
    {
        var expected = bitConverter.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
        ExpectBytes(expected);
    }
}