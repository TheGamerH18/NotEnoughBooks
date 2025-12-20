using System.Net.Mime;

namespace NotEnoughBooks.Core.Extensions;

public static class StringExtensions
{
    public static string GetFileExtension(this string mediaType)
    {
        return mediaType switch
        {
            MediaTypeNames.Image.Jpeg => ".jpg",
            MediaTypeNames.Image.Png => ".png",
            MediaTypeNames.Image.Webp => ".webp",
            _ => null
        };
    }
}