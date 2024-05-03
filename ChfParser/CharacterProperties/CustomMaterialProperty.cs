using ChfUtils;

namespace ChfParser;

public sealed class CustomMaterialProperty
{
    public required string Key { get; init; }
    public required uint ChildCount { get; init; }
    
    public required CustomMaterialChildProperty[] Children { get; init; }
    
    
    public static CustomMaterialProperty Read(ref SpanReader reader)
    {
        var key = reader.NextKey;
        if (key != "8E-9E-12-72" && key != "05-8A-37-A5")
            throw new Exception($"Invalid key: {key}");
        
        var hmmm = BitConverter.ToString(reader.ReadBytes(4).ToArray()); //reader.ExpectBytes("8E-9E-12-72");//or "05-8A-37-A5"
        //Console.WriteLine($"Hmmm: {hmmm}");

        var childCount = reader.Read<uint>();
        var children = new CustomMaterialChildProperty[childCount];
        
        for (var i = 0; i < childCount; i++)
        {
            var child = CustomMaterialChildProperty.Read(ref reader);
            
            //Console.WriteLine($"({i + 1}/{childCount}) CustomMaterialProperty: {child.Count} {Constants.GetName(child.Id)} ");
            
            children[i] = child;
        }
        
        //Console.WriteLine();

        return new CustomMaterialProperty
        {
            ChildCount = childCount,
            Children = children,
            Key = key
        };
    }
}

public sealed class CustomMaterialChildProperty
{
    public required byte Count { get; init; }
    public required Guid Id { get; init; }
    
    public static CustomMaterialChildProperty Read(ref SpanReader reader)
    {
        reader.Expect<uint>(0);
        var count = reader.Read<byte>();
        var id = reader.Read<Guid>();
        
        return new CustomMaterialChildProperty()
        {
            Count = count,
            Id = id
        };
    }
}