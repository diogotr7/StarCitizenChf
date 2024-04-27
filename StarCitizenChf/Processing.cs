namespace StarCitizenChf;

public static class Processing
{
    public static async Task ProcessAllCharacters(string charactersFolder)
    {
        await Task.WhenAll(Directory.GetDirectories(charactersFolder).Select(async characterFolder =>
        {
            try
            {
                var files = Directory.GetFiles(characterFolder);
                if (files.Length == 0)
                    return;

                var chf = files.SingleOrDefault(x => x.EndsWith(".chf"));
                if (chf == null)
                    return;
                
                var bin = Path.ChangeExtension(chf, ".bin");
                if (!File.Exists(bin))
                    await Decompression.DecompressFile(chf, bin);

                var eyeImage = Path.Combine(characterFolder, "eye.png");
                if (!File.Exists(eyeImage))
                {
                    var eyeColor = await Analysis.GetEyeColor(bin);
                    await Images.WriteSolidColorImage(eyeImage, eyeColor);
                }
                
                var reversedBin = Path.ChangeExtension(chf, ".reversed.bin");
                if (!File.Exists(reversedBin))
                    await Utils.ReverseFile(bin, reversedBin);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing {characterFolder}: {e.Message}");
            }
        }));
    }
}