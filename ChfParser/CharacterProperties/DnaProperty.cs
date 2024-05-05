using ChfUtils;

namespace ChfParser;

public sealed class DnaProperty
{
    private const int Size = 0xD8;
    
    public required string Dna { get; init; }
    
    public static DnaProperty Read(ref SpanReader reader)
    {
        reader.Expect<ulong>(Size);

        var dna = reader.ReadBytes(Size).ToArray();
        
        var dnaString = BitConverter.ToString(dna).Replace("-", "");
        
        return new DnaProperty
        {
            Dna = dnaString
        };
    }
}