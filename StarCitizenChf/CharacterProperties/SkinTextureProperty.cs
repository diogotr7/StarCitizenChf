namespace StarCitizenChf;

internal sealed class SkinTextureProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";
    
    public byte[] Data { get; set; }
    
    public static SkinTextureProperty Read(ref SpanReader reader)
    {
        var unknownData = reader.ReadBytes(sizeof(uint) * 5);
        
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(1);
        reader.Expect(5);

        return new SkinTextureProperty() { Data = unknownData.ToArray() };
    }
}