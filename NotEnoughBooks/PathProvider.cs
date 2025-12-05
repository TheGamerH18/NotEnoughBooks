using System.Reflection.PortableExecutable;

namespace NotEnoughBooks;

public class PathProvider
{
    public static string ThumbnailsFolderPath => Path.Combine(Directory.GetCurrentDirectory(), "AppData", "thumbnails");
}