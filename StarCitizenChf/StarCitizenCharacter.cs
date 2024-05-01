using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StarCitizenChf;

public sealed class StarCitizenCharacter
{
    public required string Name { get; init; }
    public required bool IsMan { get; init; }
    public required ulong TotalCount { get; init; }
    
    public required string EyesId { get; init; }
    public required string HairId { get; init; }
    public required string HairModId { get; init; }
    public required string HeadMatId { get; init; }
    public required string EyeBrowId { get; init; }
    public required string BeardId { get; init; }
    public required string BeardModId { get; init; }

    public required string Next { get; init; }
    public required ulong NextCount { get; init; }
    
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

        var totalCount = reader.Read<uint>();
        reader.Expect<uint>(0);

        var bodyKey = reader.Read<uint>();
        if (bodyKey != BodyProperty.Key)
            throw new Exception();

        var body = BodyProperty.Read(ref reader);
        //head is child of body?
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

        var eyelashKey = reader.Read<uint>();
        if (eyelashKey != EyelashProperty.Key)
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

                facialHairModifier = HairModifierProperty.Read(ref reader);
                //do something with this
            }
        }

        var headMaterialKey = reader.Read<uint>();
        if (headMaterialKey != HeadMaterialProperty.Key)
            throw new Exception();

        var headMaterial = HeadMaterialProperty.Read(ref reader);

        //unknownprop 6 or 7. i am completely lost here
        //72129E8E or A5378A05
        //nextCount is always 0

        return new StarCitizenCharacter()
        {
            Name = fileName,
            IsMan = isMan,
            TotalCount = totalCount,
            
            EyesId = GuidUtils.Shorten(eyes.Id),
            HairId = GuidUtils.Shorten(hair.Id),
            HairModId = GuidUtils.Shorten(hairModifier?.Id ?? Guid.Empty),
            HeadMatId = GuidUtils.Shorten(headMaterial.Id),
            EyeBrowId = GuidUtils.Shorten(eyebrow?.Id ?? Guid.Empty),
            BeardId = GuidUtils.Shorten(facialHair?.Id ?? Guid.Empty),
            BeardModId = GuidUtils.Shorten(facialHairModifier?.Id ?? Guid.Empty),

            Next = reader.Read<uint>().ToString("X8"),
            NextCount = 0,
        };
    }
}