using System.Security.Cryptography;

namespace StarCitizenChf;

public static class Decryption
{
    public static void Decrypt(string inputFilename, string outputFilename)
    {
        var file = File.ReadAllBytes(inputFilename);

       var bytes = file.AsSpan()[8..].ToArray();
       // var bytes = file;
        var key = new byte[] { 0x5E, 0x7A, 0x20, 0x02, 0x30, 0x2E, 0xEB, 0x1A, 0x3B, 0xB6, 0x17, 0xC3, 0x0F, 0xDE, 0x1E, 0x47 };
        using var aes = Aes.Create();
        aes.Key = key;
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.IV = new byte[16];
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.Zeros;

        
        var decryptor = aes.CreateDecryptor();
        using var ms = new MemoryStream(bytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);
        var decrypted = reader.ReadBytes(bytes.Length);
        File.WriteAllBytes(outputFilename, decrypted);
    }
}