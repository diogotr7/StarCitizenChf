using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChfParser;

public ref struct SpanReader(ReadOnlySpan<byte> span)
{
    public ReadOnlySpan<byte> Span { get; } = span;
    public int Position { get; private set; } = 0;
    
    public ReadOnlySpan<byte> Remaining => Span[Position..];
    
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
            throw new InvalidOperationException("Read an int and compare it to 0 instead");
        
        return MemoryMarshal.Read<T>(Span[Position..]);
    }
    
    public ReadOnlySpan<byte> ReadBytes(int length)
    {
        var value = Span[Position..(Position + length)];
        Position += length;
        return value;
    }
    
    public void Expect<T>(T expected) where T : unmanaged, IEquatable<T>
    {
        var value = Read<T>();

        if (!value.Equals(expected))
            throw new InvalidOperationException($"Expected {expected}, got {value} at position 0x{Position - Unsafe.SizeOf<T>():X2}");
    }

    public T ReadKeyValueAndChildCount<T>(int count, uint key) where T : unmanaged
    {
        Expect(key);
        var data = Read<T>();
        Expect(count);
        
        return data;
    }
}