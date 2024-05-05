using ChfUtils;

namespace ChfParser;

public sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;

    public required Guid Id { get; init; }
    public required uint AdditionalParams { get; init; }

    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        //these additional params are always 1-1 with the guid.
        //Each guid has a unique set of additional params.
        //Unknown what these are.
        var additionalParams = reader.Read<uint>();

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        return new HeadMaterialProperty
        {
            Id = guid,
            AdditionalParams = additionalParams
        };
    }
}