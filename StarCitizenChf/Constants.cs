using System;
using System.Collections.Generic;
using System.Linq;

namespace StarCitizenChf;

public static class Constants
{
    public static readonly Guid ModelTagM = new("25f439d5-146b-4a61-a999-a486dfb68a49");
    public static readonly Guid ModelTagF = new("d0794a94-efb0-4cad-ad38-2558b4d3c253");
    
    public static readonly Guid Brows01 = new("89ec0bbc-7daf-4b09-a98d-f8dd8df32305");
    public static readonly Guid Brows02 = new("c40183e4-659c-4e4e-8f96-70b33a3b9d67");
    public static readonly Guid Brows03 = new("6606176a-bfc4-4d24-a40a-b554fcfb8c7e");
    public static readonly Guid Brows04 = new("41a65deb-4a4c-425c-8825-e6d264ecdd4b");
    public static readonly Guid Brows05 = new("a074880a-6df2-4996-89e2-3e204a2790c2");
    public static readonly Guid Brows06 = new("03270dfe-71be-45ee-b51a-fb1dd7e67ba1");
    
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

    public static (Guid, string)[] GetAllGuids()
    {
        //reflect over this class and return all guids
        return typeof(Constants).GetFields()
            .Where(f => f.FieldType == typeof(Guid))
            .Select(f => ((Guid)f.GetValue(null), f.Name))
            .ToArray();
    }
}