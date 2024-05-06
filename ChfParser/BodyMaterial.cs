using ChfUtils;

namespace ChfParser;

public sealed class BodyMaterial
{
    public const uint Key = 0x_27_42_4D_58;
    
    public required uint AdditionalParams { get; init; }
    public required Color TorsoColor { get; init; }
    public required Color LimbColor { get; init; }
    
    public static BodyMaterial Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var isMan = guid switch
        {
            _ when guid == Constants.f_body_character_customizer => false,
            _ when guid == Constants.m_body_character_customizer => true,
            _ => throw new Exception($"Unexpected guid {guid}")
        };

        var additionalParams = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(2);
        reader.Expect<uint>(5);
        reader.Expect(isMan ? 0x_73_C9_79_A9 : 0x_31_6B_6E_4C);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);
        var c1 = reader.ReadKeyValueAndChildCount<Color>(0, 0xbd530797);
        reader.Expect<uint>(5);
        reader.Expect(isMan ? 0x_A4_1F_A1_2C : 0x_8A_5B_66_DB);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(0);
        var c2 = reader.ReadKeyValueAndChildCount<Color>(0, 0xbd530797);
        
        return new BodyMaterial
        {
            AdditionalParams = additionalParams,
            TorsoColor = c1,
            LimbColor = c2
        };
    }
}