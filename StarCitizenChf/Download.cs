using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarCitizenChf;

public static class Download
{
    private static readonly HttpClient _httpClient = new();
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    static Download()
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    }

    public static async Task DownloadAllMetadata(string filePath)
    {
        var rowsList = new List<SccCharacter>();
        var page = 1;

        while (true)
        {
            var response = await _httpClient.GetFromJsonAsync<SccRoot>(
                $"https://www.star-citizen-characters.com/api/heads?page={page++}&orderBy=latest");
            rowsList.AddRange(response!.body.rows);
            if (response.body.hasNextPage == false)
                break;
        }

        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(rowsList, _jsonOptions));
    }

    public static async Task DownloadAllCharacters(string metadataFile, string outputFolder)
    {
        var rows = JsonSerializer.Deserialize<SccCharacter[]>(await File.ReadAllTextAsync(metadataFile))!;

        await Task.WhenAll(rows.Select(async row =>
        {
            try
            {
                if (!row.dnaUrl.Contains("chf") || !row.previewUrl.Contains("jpeg"))
                    return;

                var thisCharacterFolder = Path.Combine(outputFolder, GetSafeDirectoryName(row));
                Directory.CreateDirectory(thisCharacterFolder);

                var imageFilename = row.previewUrl.Split('/').Last();
                var dnaFilename = row.dnaUrl.Split('/').Last();
                var imageFile = Path.Combine(thisCharacterFolder, imageFilename);
                var dnaFile = Path.Combine(thisCharacterFolder, dnaFilename);
                if (File.Exists(imageFile) && File.Exists(dnaFile))
                    return;

                Console.WriteLine($"Downloading {row.title}...");

                await Task.WhenAll(DownloadFileAsync(row.dnaUrl, dnaFile), DownloadFileAsync(row.previewUrl, imageFile));

                Console.WriteLine($"Downloaded {row.title}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error downloading {row.title}: {e.Message}");
            }
        }));
    }

    private static async Task DownloadFileAsync(string url, string path)
    {
        var stream = await _httpClient.GetStreamAsync(url);
        await using var fileStream = File.Create(path);
        await stream.CopyToAsync(fileStream);
    }

    public static string GetSafeDirectoryName(SccCharacter character)
    {
        var start = character.title;

        Array.ForEach([..Path.GetInvalidPathChars(), ' '], x => start = start.Replace(x, '_'));

        return $"{start}-{character.id[..8]}";
    }


    public class SccRoot
    {
        public SccBody body { get; set; }
    }

    public class SccBody
    {
        public bool hasNextPage { get; set; }
        public SccCharacter[] rows { get; set; }
    }

    public class SccCharacter
    {
        public string id { get; set; }
        public string title { get; set; }
        public string previewUrl { get; set; }
        public string dnaUrl { get; set; }
    }
}