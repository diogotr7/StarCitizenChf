namespace StarCitizenChf;

public static class Images
{
    public static async Task WriteSolidColorImage(string path, int width, int height, Rgba32 color)
    {
        using var image = new Image<Rgba32>(width, height, color);
        await using var stream = File.OpenWrite(path);
        await image.SaveAsPngAsync(stream);
    }
}