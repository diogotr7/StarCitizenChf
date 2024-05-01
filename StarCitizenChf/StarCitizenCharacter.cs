using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StarCitizenChf;

public sealed class StarCitizenCharacter
{
    public string? Name { get; init; }
    public bool? IsMan { get; init; }
    public string? Hair { get; init; }
    public uint? HairValue { get; init; }
    public string? Eyebrow { get; init; }
    public uint? Property2 { get; init; }
    public uint? Property3 { get; init; }
    public string? Beard { get; init; }
    public uint? BeardValue { get; init; }
    public string? Skin { get; init; }

    public ulong Size { get; init; }

    public string Next { get; init; }
    public ulong NextCount { get; init; }

    public static StarCitizenCharacter FromBytes(string fileName, ReadOnlySpan<byte> data)
    {
        var reader = new SpanReader(data);
        
        reader.Expect<uint>(2); //version?
        reader.Expect<uint>(7); //chf version definitely

        var gender = reader.ReadGuid();
        var isMan = gender == Constants.ModelTagM;
        var isWoman = gender == Constants.ModelTagF;
        if (!isMan && !isWoman)
            throw new Exception();

        reader.Expect<ulong>(0);
        reader.Expect<ulong>(0);

        var dnaLength = reader.Read<ulong>();
        if (dnaLength != 216)
            throw new Exception();

        var dnaByteArray = reader.ReadBytes((int)dnaLength);

        var someCount = reader.Read<uint>();
        reader.Expect<uint>(0);

        var bodyKey = reader.Read<uint>();
        if (bodyKey != BodyProperty.Key)
            throw new Exception();

        var body = BodyProperty.Read(ref reader);
        //this is probably a count of *something*
        if (body.ChildCount != 1)
            throw new Exception();

        var headKey = reader.Read<uint>();
        if (headKey != HeadProperty.Key)
            throw new Exception();

        var head = HeadProperty.Read(ref reader);

        if (head.ChildCount == 0)
            throw new Exception();

        var eyesKey = reader.Read<uint>();
        if (eyesKey != EyesProperty.Key)
            throw new Exception();

        var eyes = EyesProperty.Read(ref reader);

        if (eyes.ChildCount != 0)
            throw new Exception();

        var hairKey = reader.Read<uint>();
        if (hairKey != HairProperty.Key)
            throw new Exception();

        var hair = HairProperty.Read(ref reader);
        HairModifierProperty? hairModifier = null;

        if (hair.ChildCount == 1)
        {
            var modifierKey = reader.Read<uint>();
            if (modifierKey != HairModifierProperty.Key)
                throw new Exception();

            hairModifier = HairModifierProperty.Read(ref reader);

            //TODO move the modifier read into the HairProperty.Read method?
        }
        else if (hair.ChildCount != 0)
        {
            throw new Exception();
        }

        EyeBrowProperty? eyebrow = null;
        if (reader.PeekKey == EyeBrowProperty.Key)
        {
            _ = reader.Read<uint>();
            eyebrow = EyeBrowProperty.Read(ref reader);
            if (eyebrow.ChildCount != 0)
                throw new Exception();
        }

        var eyelasheKey = reader.Read<uint>();
        if (eyelasheKey != EyelashProperty.Key)
            throw new Exception();

        var eyelash = EyelashProperty.Read(ref reader);
        
        //When this is 0, we have beard. why?
        FacialHairProperty? facialHair = null;
        HairModifierProperty? facialHairModifier = null;
        
        if (reader.PeekKey == FacialHairProperty.Key)
        {
            _ = reader.Read<uint>();
            facialHair = FacialHairProperty.Read(ref reader);

            if (facialHair.ChildCount > 1)
                throw new Exception();
            if (facialHair.ChildCount == 1)
            {
                var hairModifierKey = reader.Read<uint>();
                if (hairModifierKey != HairModifierProperty.Key)
                    throw new Exception();

                hairModifier = HairModifierProperty.Read(ref reader);
                //do something with this
            }
        }

        var headMaterialKey = reader.Read<uint>();
        if (headMaterialKey != HeadMaterialProperty.Key)
            throw new Exception();

        var headMaterial = HeadMaterialProperty.Read(ref reader);

        //unknownprop 6 or 7. i am completely lost here

        return new StarCitizenCharacter()
        {
            Name = fileName,
            Next = reader.Read<uint>().ToString("X8"),
            NextCount = 0
        };
    }
}