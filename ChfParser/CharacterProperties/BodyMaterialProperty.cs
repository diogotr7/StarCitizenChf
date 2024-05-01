using ChfUtils;

namespace ChfParser;

public sealed class BodyMaterialProperty
{
    public const uint Key = 0x_27_42_4D_58;
    public const string KeyRep = "58-4D-42-27";
    
    public Guid Id { get; set; }
    
    public static BodyMaterialProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var alsoGuidMaybe = reader.Read<uint>();
        
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(2);
        reader.Expect(5);

        return new BodyMaterialProperty() { Id = guid };
    }
}