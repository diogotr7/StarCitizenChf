// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using System.Text.Json;

using var httpClient = new HttpClient();
int page = 1;
Directory.CreateDirectory("data");

while (true)
{
    var response = await httpClient.GetFromJsonAsync<RootObject>($"https://www.star-citizen-characters.com/api/heads?page={page}&orderBy=latest");
    await File.WriteAllTextAsync($"data/page{page}.json", JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

    page++;
    if (response!.body.hasNextPage == false)
        break;
}

var urls = Directory.GetFiles("data", "*.json")
    .SelectMany(page => JsonSerializer.Deserialize<RootObject>(File.ReadAllText(page))!.body.rows.Select(row => row.dnaUrl));

await File.WriteAllTextAsync("urls.txt", string.Join(Environment.NewLine, urls));

public record RootObject(
    Body body,
    string path,
    string query,
    object[] cookies
);

public record Body(
    bool hasPrevPage,
    bool hasNextPage,
    Rows[] rows
);

public record Rows(
    string id,
    string createdAt,
    string title,
    object[] tags,
    User user,
    string previewUrl,
    string dnaUrl,
    _count _count
);

public record User(
    string id,
    string name,
    string image,
    string starCitizenHandle
);

public record _count(
    int characterDownloads,
    int characterLikes
);

