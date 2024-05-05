namespace ChfParser;

public sealed record Key(uint val)
{
    private readonly uint _value = val;
    public string Text => BitConverter.ToString(BitConverter.GetBytes(_value));
    public uint Value => _value;
    
    public static implicit operator Key(uint val) => new Key(val);
    public static implicit operator uint(Key key) => key.Value;
    
    public static implicit operator Key(string val) => new Key(BitConverter.ToUInt32(val.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray()));
    public static implicit operator string(Key key) => key.Text;
}