using ChfUtils;

namespace ChfParser;

public sealed class CustomMaterialChildProperty
{
    public required byte Count { get; init; }
    public required Guid Id { get; init; }

    public static CustomMaterialChildProperty Read(ref SpanReader reader)
    {
        reader.Expect<uint>(0);
        var count = reader.Read<byte>();
        var id = reader.Read<Guid>();

        return new CustomMaterialChildProperty
        {
            Count = count,
            Id = id
        };
    }
}