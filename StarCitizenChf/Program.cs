using ZstdSharp;
const int size = 4096;//all files are 4096 bytes long

var buffers = Directory.GetFiles("data", "*.chf", SearchOption.AllDirectories)
    .Select(File.ReadAllBytes).ToArray();
var names = Directory.GetFiles("data", "*.chf", SearchOption.AllDirectories)
    .Select(Path.GetFileName).ToArray();

// var onesWithDeadbeef = buffers.Where(b =>
// {
//     var span = b.AsSpan();
//     ReadOnlySpan<byte> deadbeef = [0xEF, 0xBE, 0xAD, 0xDE];
//     
//     return span.IndexOf(deadbeef) != -1;
// }).ToArray();
// var onesWithoutDeadbeef = buffers.Except(onesWithDeadbeef).ToArray();
//
// var commonBytesDeadBeef = new List<int>(size);
// var commonBytesNoDeadBeef = new List<int>(size);
//
// for (var i = 0; i < size; i++)
// {
//     if (onesWithDeadbeef.All(b => b[i] == buffers[0][i]))
//         commonBytesDeadBeef.Add(i);
//     
//     if (onesWithoutDeadbeef.All(b => b[i] == buffers[0][i]))
//         commonBytesNoDeadBeef.Add(i);
// }
//
// var valuesAtFFB = buffers.Select(i => i[0xffb]).ToArray();
// for(var i = 0; i < buffers.Length; i++){
//     if(valuesAtFFB[i] == 61)
//         Console.WriteLine($"Special file: {names[i]}");
// }
// var ddbfvaluesAtBytes = commonBytesDeadBeef.Select(i => onesWithDeadbeef[0][i]).ToArray();
// var ddbfasString = string.Join(", ", commonBytesDeadBeef.Select(i => $"0x{i:X2}"));
// var ddbfvaluesAtByteHex = string.Join(", ", ddbfvaluesAtBytes.Select(b => $"0x{b:X2}"));
//
//
// var nddbfvaluesAtBytes = commonBytesNoDeadBeef.Select(i => onesWithoutDeadbeef[0][i]).ToArray();
// var nddbfasString = string.Join(", ", commonBytesNoDeadBeef.Select(i => $"0x{i:X2}"));
// var nddbfvaluesAtByteHex = string.Join(", ", nddbfvaluesAtBytes.Select(b => $"0x{b:X2}"));
Directory.CreateDirectory("decompressed");
foreach (var (buffer, name) in buffers.Zip(names))
{
    try
    {
        var useful = buffer[16..^8];
        var lastDataByte = FindLastDataByte(useful);
        if (lastDataByte == -1)
            continue;
        
        var cropped = useful[..(lastDataByte + 1)];
        using var zstd = new Decompressor();
        var decompressed = zstd.Unwrap(cropped.ToArray());
        
        Console.WriteLine($"Decompressed length: {decompressed.Length}");
        File.WriteAllBytes(Path.Combine("decompressed", name), decompressed.ToArray());
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

return;

static int FindLastDataByte(ReadOnlySpan<byte> useful)
{
    const int expectedLength = 4096 - 8 - 16;
    if (useful.Length != expectedLength) throw new ArgumentException("Span must be expectedLength bytes long", nameof(useful));
    //check for zstd magic number
    if (useful[0] != 0x28 || useful[1] != 0xB5 || useful[2] != 0x2F || useful[3] != 0xFD)
        throw new ArgumentException("Span must start with Zstd magic number", nameof(useful));

    var containsDeadbeef = useful.IndexOf<byte>([0xEF, 0xBE, 0xAD, 0xDE]) != -1;

    if (!containsDeadbeef)
        return useful.LastIndexOfAnyExcept<byte>(0x00);

    var firstDeadbeef = FindFirstDeadBeefByte(useful);
    var slice = useful[..firstDeadbeef];
    return slice.LastIndexOfAnyExcept<byte>(0x00);
}

static int FindFirstDeadBeefByte(ReadOnlySpan<byte> useful)
{
    for (var i = useful.Length - 1; i >= 0; i-=4)
    {
        if (useful[i] != 0xDE) 
            return i;
        if (useful[i - 1] != 0xAD) 
            return i - 1;
        if (useful[i - 2] != 0xBE) 
            return i - 2;
        if (useful[i - 3] != 0xEF) 
            return i - 3;
    }

    return -1;
}
