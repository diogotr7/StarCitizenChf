using System;
using ChfUtils;

namespace ChfParser;

internal sealed class DnaProperty
{
    public const int Size = 0xD8;
    
    public string Dna { get; set; }
    
    public static DnaProperty Read(ref SpanReader reader)
    {
        var dnaLength = reader.Read<ulong>();
        if (dnaLength != Size)
            throw new Exception();

        var dna = reader.ReadBytes(Size).ToArray();
        
        var dnaString = BitConverter.ToString(dna).Replace("-", "");
        
        return new DnaProperty()
        {
            Dna = dnaString
        };
    }
}