using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChfParser;

public ref struct SpanWriter(Span<byte> span)
{
    public Span<byte> Span { get; } = span;
    public int Position { get; private set; } = 0;
    
    public void Write<T>(T value) where T : unmanaged
    {
        MemoryMarshal.Write(Span[Position..], in value);
        Position += Unsafe.SizeOf<T>();
    }
}