using ChfUtils;

namespace ChfParser;

//libs/foundry/records/entities/scitem/characters/human/head/shared/hair/hair_13.xml
public sealed class HairProperty
{
    public const uint Key = 0x13601A95;

    public required HairType HairType { get; init; }
    public required HairModifierProperty? Modifier { get; init; }

    public static HairProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var type = guid switch
        {
            _ when guid == Bald01 => HairType.Bald01,
            _ when guid == Hair02 => HairType.Hair02,
            _ when guid == Hair03 => HairType.Hair03,
            _ when guid == Hair04 => HairType.Hair04,
            _ when guid == Hair05 => HairType.Hair05,
            _ when guid == Hair06 => HairType.Hair06,
            _ when guid == Hair07 => HairType.Hair07,
            _ when guid == Hair08 => HairType.Hair08,
            _ when guid == Hair09 => HairType.Hair09,
            _ when guid == Hair10 => HairType.Hair10,
            _ when guid == Hair11 => HairType.Hair11,
            _ when guid == Hair12 => HairType.Hair12,
            _ when guid == Hair13 => HairType.Hair13,
            _ when guid == Hair14 => HairType.Hair14,
            _ when guid == Hair15 => HairType.Hair15,
            _ when guid == Hair16 => HairType.Hair16,
            _ when guid == Hair17 => HairType.Hair17,
            _ when guid == Hair18 => HairType.Hair18,
            _ when guid == Hair19 => HairType.Hair19,
            _ when guid == Hair20 => HairType.Hair20,
            _ when guid == Hair21 => HairType.Hair21,
            _ when guid == Hair22 => HairType.Hair22,
            _ when guid == Hair23 => HairType.Hair23,
            _ when guid == Hair24 => HairType.Hair24,
            _ => throw new ArgumentOutOfRangeException(nameof(guid), guid, null)
        };
        var childCount = reader.Read<ulong>();

        switch (childCount)
        {
            case 0:
                return new HairProperty
                {
                    HairType = type,
                    Modifier = null
                };
            case 1:
            {
                var hairModifier = HairModifierProperty.Read(ref reader);
                if (hairModifier.ChildCount != 0)
                    throw new Exception("HairModifierProperty child count is not 0");

                return new HairProperty
                {
                    HairType = type,
                    Modifier = hairModifier
                };
            }
            default: throw new Exception("HairProperty child count is not 0 or 1");
        }
    }

    public static readonly Guid Bald01 = new("71dd6cea-e225-4aaf-b9d7-562d2083ae3b");
    public static readonly Guid Hair02 = new("968d0d95-2224-47dc-a05a-da423a4a1c81");
    public static readonly Guid Hair03 = new("a8beac2a-3a3a-455b-9d2b-cfcadf290419");
    public static readonly Guid Hair04 = new("e35c9137-dc5b-457f-b86a-f4f47d4ea96a");
    public static readonly Guid Hair05 = new("78ba4b65-a6a6-4c08-b78a-4d4b0adc020d");
    public static readonly Guid Hair06 = new("041df6b0-2498-4799-a7a5-d2d40856c409");
    public static readonly Guid Hair07 = new("57eedc17-d982-485f-98bc-161df3822022");
    public static readonly Guid Hair08 = new("7222de26-f519-4d98-99f7-0aa602ae4c05");
    public static readonly Guid Hair09 = new("ebd09681-1909-4047-8989-774974da71b7");
    public static readonly Guid Hair10 = new("9b688d20-e494-4c80-b0b7-e2989ddd4cbc");
    public static readonly Guid Hair11 = new("ad65b2a4-6ee8-4f9f-ac86-0b1725e3e5a1");
    public static readonly Guid Hair12 = new("8b40a194-fc32-4668-bd77-dc7e54708725");
    public static readonly Guid Hair13 = new("f2343738-d0bc-4a99-bd11-98aa9cd5063d");
    public static readonly Guid Hair14 = new("b0ef56d6-fafb-4b52-8713-2c00577605a5");
    public static readonly Guid Hair15 = new("bc8dbe98-2990-46ee-ac08-268dddb15bd7");
    public static readonly Guid Hair16 = new("f6a28414-2326-41e1-a9f0-ad76600b4f5b");
    public static readonly Guid Hair17 = new("74a0634c-0ca6-4e8c-a0fc-21cfa6a0663b");
    public static readonly Guid Hair18 = new("854f7b6c-7054-4f51-867c-f5fe8247b884");
    public static readonly Guid Hair19 = new("73005464-e866-44f6-a192-c091bcb15fb3");
    public static readonly Guid Hair20 = new("c0ff663f-006f-4668-9a7a-2c125c55e291");
    public static readonly Guid Hair21 = new("ce9137d2-9c06-412a-ab90-5da4e988902b");
    public static readonly Guid Hair22 = new("d7cb9d99-2e76-43ab-b21e-7c0f9f1df419");
    public static readonly Guid Hair23 = new("63a60790-fc1c-47bb-b0df-d1452e8cde2b");
    public static readonly Guid Hair24 = new("03762539-c42e-4314-9710-97430c72da98");
}

public enum HairType
{
    None,
    Bald01,
    Hair02,
    Hair03,
    Hair04,
    Hair05,
    Hair06,
    Hair07,
    Hair08,
    Hair09,
    Hair10,
    Hair11,
    Hair12,
    Hair13,
    Hair14,
    Hair15,
    Hair16,
    Hair17,
    Hair18,
    Hair19,
    Hair20,
    Hair21,
    Hair22,
    Hair23,
    Hair24
}