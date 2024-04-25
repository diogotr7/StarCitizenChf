using System.Security.Cryptography;

namespace StarCitizenChf;

public static class Decryption
{
    public static void Decrypt(string inputFilename, string outputFilename)
    {
        var file = File.ReadAllBytes(inputFilename);

        var bytes = file.AsSpan()[8..].ToArray();
        using var aes = Aes.Create();

        aes.Key = [0x5E, 0x7A, 0x20, 0x02, 0x30, 0x2E, 0xEB, 0x1A, 0x3B, 0xB6, 0x17, 0xC3, 0x0F, 0xDE, 0x1E, 0x47];
        aes.IV = new byte[16];
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.None;

        var decryptor = aes.CreateDecryptor();
        using var ms = new MemoryStream(bytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);
        var decrypted = reader.ReadBytes(bytes.Length);
        File.WriteAllBytes(outputFilename, decrypted);
    }

    public static void DecryptAll(string inputDirectory)
    {
        var binFiles = Directory.GetFiles(inputDirectory, "*.bin", SearchOption.AllDirectories);

        foreach (var binFile in binFiles)
        {
            try
            {
                Decrypt(binFile, Path.ChangeExtension(binFile, ".decrypted"));
                Console.WriteLine($"Decrypted {binFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    
}