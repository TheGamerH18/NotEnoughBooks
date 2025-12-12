namespace NotEnoughBooks;

public class PathProvider
{
    public static string ThumbnailsFolderPath => Path.Combine(Directory.GetCurrentDirectory(), "AppData", "thumbnails");
    public static string AdminConfigurationFile => Path.Combine(Directory.GetCurrentDirectory(), "AppData", "AdminConfiguration.json");
}