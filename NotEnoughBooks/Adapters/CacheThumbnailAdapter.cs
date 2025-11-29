using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class CacheThumbnailAdapter : ICacheThumbnailPort
{
    private readonly HttpClient _httpClient;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<CacheThumbnailAdapter> _logger;
    
    public async Task<string> SaveThumbnail(string url, Guid bookId)
    {
        try
        {
            string folder = Path.Combine(_environment.WebRootPath, "thumbnails");
            Directory.CreateDirectory(folder);
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogWarning("Could not retrieve thumbnail");
                return string.Empty;
            }

            if (!TryGetFileExtension(httpResponseMessage, out string extension)) 
                return string.Empty;
            string path = Path.Combine(folder, bookId+extension);

            await using (Stream streamAsync = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                await using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await streamAsync.CopyToAsync(fileStream);
                }
            }
            return "/thumbnails/" + bookId+extension;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save thumbnail");
            return string.Empty;
        }
    }

    private bool TryGetFileExtension(HttpResponseMessage httpResponseMessage, out string extension)
    {
        string contentType = httpResponseMessage.Content.Headers.ContentType?.MediaType;
        switch (contentType)
        {
            case "image/jpg":
            case "image/jpeg":
                extension = ".jpg";
                break;
            case "image/png":
                extension = ".png";
                break;
            default:
                _logger.LogWarning("Could not determine/Forbidden file extension");
                extension = string.Empty;
                return false;
        }

        return true;
    }
}