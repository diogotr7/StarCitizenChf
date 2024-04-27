namespace StarCitizenChf;

public static class Images
{
    public static async Task WriteSolidColorImage(string path, Rgba32 color)
    {
        using var image = new Image<Rgba32>(256, 256, color);
        await using var stream = File.OpenWrite(path);
        await image.SaveAsPngAsync(stream);
    }
}