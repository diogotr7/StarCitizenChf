namespace ChfParser;

public sealed class DnaProperty
{
    private const int Size = 0xD8;
    
    public required string DnaString { get; init; }
    
    public static DnaProperty Read(ref SpanReader reader, BodyType bodyType)
    {
        reader.Expect<ulong>(Size);

        var dna = reader.ReadBytes(Size).ToArray();
        //DnaDebug.Debug(dna, bodyType == BodyType.Male);
        
        var dnaString = BitConverter.ToString(dna).Replace("-", "");
        
        return new DnaProperty
        {
            DnaString = dnaString
        };
    }
}