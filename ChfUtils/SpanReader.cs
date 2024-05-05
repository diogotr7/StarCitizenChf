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
        
        if (typeof(T) == typeof(bool))
        {
            var value = Peek<int>();
            //if the value is 0, return false, otherwise return true
            var isTrue = value != 0;
            return Unsafe.As<bool, T>(ref isTrue);
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
    
    public ReadOnlySpan<byte> PeekBytes(int length)
    {
        return Span[Position..(Position + length)];
    }
    
    /// <summary>
    ///     Reads a T value from the span and checks if it matches the expected value.
    /// </summary>
    public T Expect<T>(T expected) where T : unmanaged, IEquatable<T>
    {
        var value = Read<T>();
        
        if (!value.Equals(expected))
        {
            Debugger.Break();
            throw new InvalidOperationException($"Expected {expected}, got {value} at position 0x{Position - Unsafe.SizeOf<T>():X2}");
        }

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
        {
            Debugger.Break();
            throw new InvalidOperationException($"Expected {BitConverter.ToString(expected.ToArray())}, got {BitConverter.ToString(value.ToArray())} at position 0x{Position - expected.Length:X2}");
        }
    }
    
    public ReadOnlySpan<byte> ReadUntil(ReadOnlySpan<byte> expected)
    {
        var index = Remaining.IndexOf(expected);
        if (index == -1)
            return [];
        
        var value = Span[Position..(Position + index)];
        Position += index + expected.Length;
        return value;
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
    
    public Guid ReadKeyAndGuid(params string[] acceptableKeys)
    {
        var nextKey = NextKey;
        if (acceptableKeys.Length > 0 && !acceptableKeys.Contains(nextKey))
            throw new Exception($"Unexpected key: {nextKey}");
        //if no key is provided, assume any key is acceptable

        ExpectBytes(nextKey);
        return Read<Guid>();
    }

    public T ReadKeyValueAndChildCount<T>(int count, params string[] acceptableKeys) where T : unmanaged
    {
        var nextKey = NextKey;
        if (acceptableKeys.Length > 0 && !acceptableKeys.Contains(nextKey))
        {
            Debugger.Break();
            throw new Exception($"Unexpected key: {nextKey}");
        }

        ExpectBytes(nextKey);
        var data = Read<T>();
        Expect(count);
        return data;
    }
}