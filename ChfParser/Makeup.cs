
namespace ChfParser;

public sealed class Makeup
{
    public required byte Count { get; init; }
    public required MakeupType Type { get; init; }

    public static Makeup Read(ref SpanReader reader)
    {
        reader.Expect<uint>(0);
        var count = reader.Read<byte>();
        var id = reader.Read<Guid>();
        
        var type = id switch
        {
            _ when id == Guid.Empty => MakeupType.None,
            _ when id == Eyes01Id => MakeupType.Eyes01,
            _ when id == Eyes02Id => MakeupType.Eyes02,
            _ when id == Eyes03Id => MakeupType.Eyes03,
            _ when id == Eyes04Id => MakeupType.Eyes04,
            _ when id == Eyes05Id => MakeupType.Eyes05,
            _ when id == Lips01Id => MakeupType.Lips01,
            _ when id == Lips02Id => MakeupType.Lips02,
            _ when id == Lips03Id => MakeupType.Lips03,
            _ when id == Lips04Id => MakeupType.Lips04,
            _ when id == Lips05Id => MakeupType.Lips05,
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };

        return new Makeup
        {
            Count = count,
            Type = type
        };
    }
    
    public static readonly Guid Eyes01Id = new("b643f3b3-21bc-4f44-95e5-0de140fd7954");
    public static readonly Guid Eyes02Id = new("229a46c9-b9e5-4da2-875e-8f007642e52c");
    public static readonly Guid Eyes03Id = new("34e882d0-ae6b-4747-acf1-0a86ef8a64bb");
    public static readonly Guid Eyes04Id = new("a817c68e-8a9b-4887-b8be-1760a2a42b4d");
    public static readonly Guid Eyes05Id = new("438aa947-2c7b-41ea-86a9-71046ca59037");

    public static readonly Guid Lips01Id = new("8f19d3cd-2bf4-45ec-8189-7780a82c6e48");
    public static readonly Guid Lips02Id = new("63d0a0c7-924e-4274-af62-765d4ca4d2b4");
    public static readonly Guid Lips03Id = new("521a1b21-8bb7-44ef-91b5-74d9a3f0cf1b");
    public static readonly Guid Lips04Id = new("5f213adc-04e0-44c4-bd5a-fb6a07022c70");
    public static readonly Guid Lips05Id = new("db723134-9142-43c1-84c0-ace36c176135");
    
    //unused?
    public static readonly Guid MakeupFoundation01 = new("b5e53e65-bd4a-4f50-bcd1-843ce5fc231b");
    public static readonly Guid MakeupFoundation02 = new("318114ee-f184-42f5-86cb-19a321bcb513");
    public static readonly Guid MakeupFoundation03 = new("9254513e-8996-4ffb-84f0-7eb6162dddf5");
    public static readonly Guid MakeupFoundation04 = new("846d7afe-2725-47ff-a4b0-c6bdb0aaeade");
    public static readonly Guid BlemishExample_mask_mask = new("13cfead6-5662-4dea-995a-5e3f42460e20");
    public static readonly Guid BlemishExample_ID_mask = new("2d8cdf2c-5e5b-482f-ab7c-67e56e2115ae");
}

public enum MakeupType
{
    None,
    Eyes01,
    Eyes02,
    Eyes03,
    Eyes04,
    Eyes05,
    Lips01,
    Lips02,
    Lips03,
    Lips04,
    Lips05,
}