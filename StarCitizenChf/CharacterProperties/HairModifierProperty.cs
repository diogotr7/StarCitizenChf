using System;
using System.Diagnostics;

namespace StarCitizenChf;

//libs/foundry/records/entities/scitem/characters/human/appearance_modifier/hair_variant/hair_var_brown.xml
internal sealed class HairModifierProperty
{
    public const uint Key = 0x_e7_80_9d_46;
    public const string KeyRep = "46-9D-80-E7";
    
    public uint Data { get; set; }
    
    public static HairModifierProperty Read(ref SpanReader reader)
    {
        var parent = reader.PeekBehind(sizeof(uint) * 8, 4);
        var uintValue = BitConverter.ToString(parent.ToArray());
        
        Console.WriteLine($"Parent of UnknownProperty2: {uintValue}");
        
        var guid = reader.ReadGuid();
        reader.Expect<uint>(0);
        var count = reader.Read<uint>();//usually 0 sometimes 6
        if (count == 0)
            return new HairModifierProperty() { Data = count };//we are done, read next property
        if (count != 6) Debugger.Break();

        //the data i have has this 5 here but the next property is right after.
        //Unknown what this is.
        //count of how many objects are left to read??
        reader.Expect(5);
        return new HairModifierProperty() { Data = count };
    }
}