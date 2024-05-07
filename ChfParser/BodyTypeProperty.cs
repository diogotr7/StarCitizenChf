
namespace ChfParser;

public sealed class BodyTypeProperty
{
    public static readonly Guid Male = new("25f439d5-146b-4a61-a999-a486dfb68a49");
    public static readonly Guid Female = new("d0794a94-efb0-4cad-ad38-2558b4d3c253");
    
    public required BodyType Type { get; init; }
    
    public static BodyTypeProperty Read(ref SpanReader reader)
    {
        var guid = reader.Read<Guid>();
        reader.Expect(Guid.Empty);
        var bodyType = guid switch
        {
            _ when guid == Male => BodyType.Male,
            _ when guid == Female => BodyType.Female,
            _ => throw new Exception($"Unexpected guid {guid}")
        };
        
        return new BodyTypeProperty { Type = bodyType };
    }
}

public enum BodyType
{
    Male, 
    Female,
}