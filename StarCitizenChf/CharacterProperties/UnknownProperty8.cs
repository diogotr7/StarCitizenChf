namespace StarCitizenChf;

internal sealed class UnknownProperty8
{
    public const uint Key = 0xA20C0000;
    public const string KeyRep = "00-00-0C-A2";
    
    //1141637120 0x440C0000 similar. 3 constant ints and then the weird 0-19 thing
    
    public static UnknownProperty8 Read(ref SpanReader reader)
    {
        //I have no idea at all what to do here, misaligned :(
        
        var guid = reader.ReadGuid();

        return new UnknownProperty8();
        
        reader.ExpectBytes("4D-E5-B9-C9");
        reader.ExpectBytes("46-9A-22-2C");
        reader.ExpectBytes("E5-42-76-00");

        //why are these next two different. sometimes one sometimes the other
        reader.ExpectBytes("8F-5E-87-00");
        reader.ExpectBytes("8F-5E-87-19");

        return new UnknownProperty8();
    }
}