using System;
using ChfUtils;

namespace ChfParser;

public sealed class HeadMaterialProperty
{
    public const uint Key = 0x_A9_8B_EB_34;
    public const string KeyRep = "34-EB-8B-A9";
    
    public Guid Id { get; set; }
    
    public static HeadMaterialProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var additionalParams = reader.Read<uint>();
        
        //i have no idea what this is, but after it things seem to line up better?
        var skip = reader.ReadBytes(74);
        
        TestParser.Read(ref reader);

        return new HeadMaterialProperty() { Id = guid };
    }
}