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
        var alsoGuidMaybe = reader.Read<uint>();
        
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(0);
        reader.Expect(1);
        reader.Expect(5);
        //05-8A-37-A5 OR 8E-9E-12-72
        //var skip1 = reader.ReadBytes(4);
        //var skip2 = reader.ReadBytes(54);

        return new HeadMaterialProperty() { Id = guid };
    }
}