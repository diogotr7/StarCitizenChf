using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChfUtils;

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

                var reversedBin = Path.ChangeExtension(chf, ".rev");
                if (!File.Exists(reversedBin))
                    await Utils.ReverseFile(bin, reversedBin);

                // var eyeImage = Path.Combine(characterFolder, "eye.png");
                // if (!File.Exists(eyeImage))
                // {
                //     var eyeColor = await Analysis.GetEyeColor(bin);
                //     await Utils.WriteSolidColorImage(eyeImage, eyeColor);
                // }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing {characterFolder}: {e.Message}");
            }
        }));
    }
}