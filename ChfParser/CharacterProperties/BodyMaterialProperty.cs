using ChfUtils;

namespace ChfParser;

internal sealed class BodyMaterialProperty
{
    public const uint Key = 0x_27_42_4D_58;
    public const string KeyRep = "58-4D-42-27";
    
    public Guid Id { get; set; }
    
    public static BodyMaterialProperty Read(ref SpanReader reader)
    {
        var key = reader.Read<uint>();
        if (key != Key)
            throw new Exception();
        
        var guid = reader.ReadGuid();
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