namespace StarCitizenChf;

internal sealed class UnknownProperty4
{
    public const uint Key = 0xC5BB5550;
    public const string KeyRep = "50-55-BB-C5";
    
    public uint ChildCount { get; set; }

    public static UnknownProperty4 Read(ref SpanReader reader)
    {
        reader.ExpectBytes("71-48-60-E1");
        reader.ExpectBytes("63-A3-4C-6B");
        reader.ExpectBytes("10-03-B4-67");
        reader.ExpectBytes("74-E4-09-B7");
        
        var childCount = reader.Read<uint>();
        reader.Expect(0);

        return new UnknownProperty4()
        {
            ChildCount = childCount
        };
    }
}