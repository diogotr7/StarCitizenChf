namespace StarCitizenChf;

internal sealed class UnknownProperty1
{
    public const uint Key = 0x47010DB9;
    public const string KeyRep = "B9-0D-01-47";
    
    public uint ChildCount { get; set; }

    public static UnknownProperty1 Read(ref SpanReader reader)
    {
        reader.ExpectBytes("50-45-80-BF");
        reader.ExpectBytes("B3-FA-5C-1D");
        reader.ExpectBytes("6E-08-A7-96");
        reader.ExpectBytes("E8-39-AB-B4");
        
        var childCount = reader.Read<uint>();
        reader.Expect(0);

        return new UnknownProperty1()
        {
            ChildCount = childCount
        };
    }
}