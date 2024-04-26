﻿namespace StarCitizenChf;

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

                var hex = Path.ChangeExtension(chf, ".txt");
                if (!File.Exists(hex))
                    await HexView.ConvertToHexView(bin, hex, 1);
                
                var eyeImage = Path.Combine(characterFolder, "eye.png");
                if (!File.Exists(eyeImage))
                {
                    var eyeColor = await Analysis.GetEyeColor(bin);
                    await Images.WriteSolidColorImage(eyeImage, 64, 64, eyeColor);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing {characterFolder}: {e.Message}");
            }
        }));
    }
}