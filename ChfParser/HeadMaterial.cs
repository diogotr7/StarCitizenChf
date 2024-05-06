using ChfUtils;

namespace ChfParser;

public sealed class HeadMaterial
{
    public const uint Key = 0xA98BEB34;

    public required HeadMaterialType Material { get; init; }
    public required uint AdditionalParams { get; init; }

    public static HeadMaterial Read(ref SpanReader reader)
    {
        reader.Expect(Key);
        var guid = reader.Read<Guid>();
        var additionalParams = reader.Read<uint>();

        var type = guid switch
        {
            _ when guid == HeadMaterialM01T2 => HeadMaterialType.HeadMaterialM01,
            _ when guid == HeadMaterialM02T2 => HeadMaterialType.HeadMaterialM02,
            _ when guid == HeadMaterialM04T2 => HeadMaterialType.HeadMaterialM04,
            _ when guid == HeadMaterialM05T2 => HeadMaterialType.HeadMaterialM05,
            _ when guid == HeadMaterialM06T2 => HeadMaterialType.HeadMaterialM06,
            _ when guid == HeadMaterialM07T2 => HeadMaterialType.HeadMaterialM07,
            _ when guid == HeadMaterialM08T2 => HeadMaterialType.HeadMaterialM08,
            _ when guid == HeadMaterialM09T1 => HeadMaterialType.HeadMaterialM09,
            _ when guid == HeadMaterialM10T2 => HeadMaterialType.HeadMaterialM10,
            _ when guid == HeadMaterialM11T2 => HeadMaterialType.HeadMaterialM11,
            _ when guid == HeadMaterialM12T2 => HeadMaterialType.HeadMaterialM12,
            _ when guid == HeadMaterialM13T1 => HeadMaterialType.HeadMaterialM13,
            _ when guid == HeadMaterialM14T1 => HeadMaterialType.HeadMaterialM14,
            _ when guid == HeadMaterialM15T2 => HeadMaterialType.HeadMaterialM15,
            _ when guid == HeadMaterialF01 => HeadMaterialType.HeadMaterialF01,
            _ when guid == HeadMaterialF02 => HeadMaterialType.HeadMaterialF02,
            _ when guid == HeadMaterialF03 => HeadMaterialType.HeadMaterialF03,
            _ when guid == HeadMaterialF04 => HeadMaterialType.HeadMaterialF04,
            _ when guid == HeadMaterialF05 => HeadMaterialType.HeadMaterialF05,
            _ when guid == HeadMaterialF06 => HeadMaterialType.HeadMaterialF06,
            _ when guid == HeadMaterialF07 => HeadMaterialType.HeadMaterialF07,
            _ when guid == HeadMaterialF08 => HeadMaterialType.HeadMaterialF08,
            _ when guid == HeadMaterialF09 => HeadMaterialType.HeadMaterialF09,
            _ when guid == HeadMaterialF10 => HeadMaterialType.HeadMaterialF10,
            _ when guid == HeadMaterialF11 => HeadMaterialType.HeadMaterialF11,
            _ when guid == HeadMaterialF12 => HeadMaterialType.HeadMaterialF12,
            _ when guid == HeadMaterialF13 => HeadMaterialType.HeadMaterialF13,
            _ when guid == HeadMaterialF14 => HeadMaterialType.HeadMaterialF14,
            _ => throw new ArgumentOutOfRangeException(nameof(guid), guid, null)
        };

        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(1);
        reader.Expect<uint>(5);

        return new HeadMaterial
        {
            Material = type,
            AdditionalParams = additionalParams
        };
    }

    public static readonly Guid HeadMaterialM01T2 = new("bc56197f-ec97-43fb-b047-aaf51c8eb3b6");
    public static readonly Guid HeadMaterialM02T2 = new("2fcd7cc1-a46d-4065-84ba-bfabf9d567ce");
    public static readonly Guid HeadMaterialM04T2 = new("9c55cd1d-b397-4886-b1a4-bc38575916fd");
    public static readonly Guid HeadMaterialM05T2 = new("d9c34b15-40cd-49b1-84bb-a6161bfa5240");
    public static readonly Guid HeadMaterialM06T2 = new("538ab6c3-8bb6-4768-9ad1-cc6387e9c65f");
    public static readonly Guid HeadMaterialM07T2 = new("e6cb61c7-7740-46b9-9f9c-fd5eb3498e75");
    public static readonly Guid HeadMaterialM08T2 = new("e76ed31e-9ef4-4fe0-8a46-2c3ed8c6ab1b");
    public static readonly Guid HeadMaterialM09T1 = new("1d33cab4-50bf-4e7d-8c75-ef56e5e8a1b1");
    public static readonly Guid HeadMaterialM10T2 = new("6a7a8295-f9e4-4d98-82aa-7443adc3c6e2");
    public static readonly Guid HeadMaterialM11T2 = new("9a66730e-512e-4d21-8ba3-d3ce2c3ebfe6");
    public static readonly Guid HeadMaterialM12T2 = new("003367a7-9873-4a8f-9a27-9b8def193b43");
    public static readonly Guid HeadMaterialM13T1 = new("7e033967-fa65-423e-ba74-af2e810e4cac");
    public static readonly Guid HeadMaterialM14T1 = new("38219031-5c5a-4d44-9cb1-da8bdc0f2089");
    public static readonly Guid HeadMaterialM15T2 = new("4f79d0fb-389f-48c5-ba3b-9f290b8b4dc2");
    public static readonly Guid HeadMaterialF01 = new("6bf5cf88-c6bf-44ec-8e98-fd513c588886");
    public static readonly Guid HeadMaterialF02 = new("023bd1d1-6700-4889-b235-d3254db0cec1");
    public static readonly Guid HeadMaterialF03 = new("23795209-f1c8-42f3-8f93-5eee45c3ea34");
    public static readonly Guid HeadMaterialF04 = new("aa8cb288-e754-446a-b8f0-98107ad9914e");
    public static readonly Guid HeadMaterialF05 = new("9c6a7a36-f952-4cdc-8264-c9b83393ee2e");
    public static readonly Guid HeadMaterialF06 = new("2b23bbfa-aa4b-47e9-9bc8-2af7a2fc39ba");
    public static readonly Guid HeadMaterialF07 = new("c5b4f677-be97-4827-95b0-ffcef7b77ba8");
    public static readonly Guid HeadMaterialF08 = new("6739da5b-8d22-4114-acc1-4f333f983101");
    public static readonly Guid HeadMaterialF09 = new("983f7a30-0528-409a-9e33-1eb81a65f0e6");
    public static readonly Guid HeadMaterialF10 = new("79adf215-136a-4fc5-9dd7-9e03879e3bd8");
    public static readonly Guid HeadMaterialF11 = new("5d629e70-ff2f-4fc8-829c-b989f5494d4d");
    public static readonly Guid HeadMaterialF12 = new("24c9f393-3240-4bd3-a13a-078abd68375b");
    public static readonly Guid HeadMaterialF13 = new("35b1f87f-14e7-4ece-acf0-6d8d436941b9");
    public static readonly Guid HeadMaterialF14 = new("e186048a-9a81-47b3-828e-71e957c65762");
    
    public static readonly Guid HeadMaterialM10T2_old = new("8a3f884e-4cbf-4c49-a64d-3170e95e54b8");
}

public enum HeadMaterialType
{
    HeadMaterialM01,
    HeadMaterialM02,
    HeadMaterialM04,
    HeadMaterialM05,
    HeadMaterialM06,
    HeadMaterialM07,
    HeadMaterialM08,
    HeadMaterialM09,
    HeadMaterialM10,
    HeadMaterialM11,
    HeadMaterialM12,
    HeadMaterialM13,
    HeadMaterialM14,
    HeadMaterialM15,
    HeadMaterialF01,
    HeadMaterialF02,
    HeadMaterialF03,
    HeadMaterialF04,
    HeadMaterialF05,
    HeadMaterialF06,
    HeadMaterialF07,
    HeadMaterialF08,
    HeadMaterialF09,
    HeadMaterialF10,
    HeadMaterialF11,
    HeadMaterialF12,
    HeadMaterialF13,
    HeadMaterialF14,
}