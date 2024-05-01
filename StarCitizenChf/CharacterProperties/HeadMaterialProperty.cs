namespace StarCitizenChf;

internal sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";
    
    public byte[] Data { get; set; }
    
    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        var guid = reader.ReadGuid();
        var unknownData = reader.ReadBytes(sizeof(uint));
        
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(1);
        reader.Expect(5);

        return new HeadMaterialProperty() { Data = unknownData.ToArray() };
    }
}