using ChfUtils;

namespace ChfParser;

public sealed class FacialHairProperty
{
    public const uint Key = 0x98EFBB1C;

    public required FacialHairType FacialHairType { get; init; }
    public required HairModifierProperty? Modifier { get; init; }

    public static FacialHairProperty Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var type = guid switch
        {
            _ when guid == Beard01 => FacialHairType.Beard01,
            _ when guid == Beard02 => FacialHairType.Beard02,
            _ when guid == Beard03 => FacialHairType.Beard03,
            _ when guid == Beard04 => FacialHairType.Beard04,
            _ when guid == Beard05 => FacialHairType.Beard05,
            _ when guid == Beard06 => FacialHairType.Beard06,
            _ when guid == Beard07 => FacialHairType.Beard07,
            _ when guid == Beard08 => FacialHairType.Beard08,
            _ when guid == Beard09 => FacialHairType.Beard09,
            _ when guid == Beard10 => FacialHairType.Beard10,
            _ when guid == Beard11 => FacialHairType.Beard11,
            _ when guid == Beard12 => FacialHairType.Beard12,
            _ when guid == Beard13 => FacialHairType.Beard13,
            _ when guid == Beard14 => FacialHairType.Beard14,
            _ when guid == Beard15 => FacialHairType.Beard15,
            _ when guid == Beard16 => FacialHairType.Beard16,
            _ when guid == Beard17 => FacialHairType.Beard17,
            _ when guid == Beard18 => FacialHairType.Beard18,
            _ when guid == Beard19 => FacialHairType.Beard19,
            _ when guid == Beard20 => FacialHairType.Beard20,
            _ when guid == Beard21 => FacialHairType.Beard21,
            _ when guid == Beard22 => FacialHairType.Beard22,
            _ when guid == Beard23 => FacialHairType.Beard23,
            _ when guid == Beard24 => FacialHairType.Beard24,
            _ when guid == Beard25 => FacialHairType.Beard25,
            _ when guid == Beard26 => FacialHairType.Beard26,
            _ when guid == Beard27 => FacialHairType.Beard27,
            _ when guid == Beard28 => FacialHairType.Beard28,
            _ when guid == Beard29 => FacialHairType.Beard29,
            _ when guid == Beard30 => FacialHairType.Beard30,
            _ => throw new ArgumentOutOfRangeException(nameof(guid), guid, null)
        };

        var count = reader.Read<uint>();
        switch (count)
        {
            case 0:
                var cnt = reader.Read<uint>();
                if (cnt != 5 && cnt != 6)
                    throw new Exception();

                reader.Expect(5);
                return new FacialHairProperty { FacialHairType = type, Modifier = null };
            case 1:
                reader.Expect<uint>(0);

                var hairModifier = HairModifierProperty.Read(ref reader);

                return new FacialHairProperty { FacialHairType = type, Modifier = hairModifier };
            default:
                throw new Exception();
        }
    }

    public static readonly Guid Beard01 = new("3df7bdc3-ea80-47db-bbb7-8a6f28701c3e");
    public static readonly Guid Beard02 = new("e6c2c999-3731-4163-9f1a-e37df9b9a267");
    public static readonly Guid Beard03 = new("c9e19547-ba61-473f-b9b6-e5b8a14cb57e");
    public static readonly Guid Beard04 = new("a55229ce-89bb-405c-a225-e507e7de07ef");
    public static readonly Guid Beard05 = new("3545b83d-46c3-45d1-965a-3cfc15136ea3");
    public static readonly Guid Beard06 = new("ffb50f1a-cf01-4275-8216-71df949a2a9c");
    public static readonly Guid Beard07 = new("adc4a371-415f-4cbb-8873-569d4aec2b23");
    public static readonly Guid Beard08 = new("6c77f900-4da0-4378-891a-387d4c2572d2");
    public static readonly Guid Beard09 = new("15cc8f8d-2172-47d9-a94d-a7d504b9024d");
    public static readonly Guid Beard10 = new("184e3d83-9a08-4a6a-afd0-311836cc760e");
    public static readonly Guid Beard11 = new("59bb4f34-dd05-4f40-93f9-0161642598e0");
    public static readonly Guid Beard12 = new("b075b2cf-16a4-4152-954b-02f9fdaf3ea5");
    public static readonly Guid Beard13 = new("ee80bd3d-3b43-4ca3-b2de-e519efd4c9d7");
    public static readonly Guid Beard14 = new("f3248be1-955f-429b-ab7b-9634a531e253");
    public static readonly Guid Beard15 = new("e7eeecde-9633-481e-8b4c-f0e45a1a7e03");
    public static readonly Guid Beard16 = new("34d313db-db6f-45b0-82d9-25ae51fa7df0");
    public static readonly Guid Beard17 = new("2d459cc1-f95d-4baa-a118-cbc05bca9d97");
    public static readonly Guid Beard18 = new("2dec0896-245b-4ab1-b13d-44b0046c6774");
    public static readonly Guid Beard19 = new("547dcea1-2559-4f54-af6a-e7f025552e96");
    public static readonly Guid Beard20 = new("7b5556a2-c331-4192-b1f9-0941d778b7e3");
    public static readonly Guid Beard21 = new("8590e357-d3a7-4d39-88ff-d199ecb236c7");
    public static readonly Guid Beard22 = new("c5b7f31a-f0b5-4229-91c3-be2f859966fa");
    public static readonly Guid Beard23 = new("8ce83349-a151-4d98-ab07-d710bb8bc5fc");
    public static readonly Guid Beard24 = new("92b25f3c-740d-47ec-aae6-f2f20b2ecf68");
    public static readonly Guid Beard25 = new("88c8eee7-42cf-4baf-8286-c736a2c1bd93");
    public static readonly Guid Beard26 = new("4e1f14bc-3db8-483b-ab97-c948d8e984b8");
    public static readonly Guid Beard27 = new("bda4e041-2f54-4ec6-9bd8-6807e80ddfd6");
    public static readonly Guid Beard28 = new("e27a835b-965f-487b-bef7-ac8be077cc62");
    public static readonly Guid Beard29 = new("09b25ba2-4e5d-4135-8d27-5649227b7a74");
    public static readonly Guid Beard30 = new("31de0f7c-a059-4a5c-8917-d699a79af303");
}

public enum FacialHairType
{
    None,
    Beard01,
    Beard02,
    Beard03,
    Beard04,
    Beard05,
    Beard06,
    Beard07,
    Beard08,
    Beard09,
    Beard10,
    Beard11,
    Beard12,
    Beard13,
    Beard14,
    Beard15,
    Beard16,
    Beard17,
    Beard18,
    Beard19,
    Beard20,
    Beard21,
    Beard22,
    Beard23,
    Beard24,
    Beard25,
    Beard26,
    Beard27,
    Beard28,
    Beard29,
    Beard30
}