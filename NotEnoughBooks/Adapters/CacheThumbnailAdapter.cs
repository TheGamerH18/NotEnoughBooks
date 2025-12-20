using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Extensions;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class CacheThumbnailAdapter : ICacheThumbnailPort
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CacheThumbnailAdapter> _logger;


    public async Task<string> SaveThumbnail(string url, Guid bookId)
    {
        try
        {
            Directory.CreateDirectory(PathProvider.ThumbnailsFolderPath);
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogWarning("Could not retrieve thumbnail");
                return string.Empty;
            }

            string extension = httpResponseMessage.Content.Headers.ContentType?.MediaType.GetFileExtension();
            if (extension == null)
                return string.Empty;
            
            string path = Path.Combine(PathProvider.ThumbnailsFolderPath, bookId + extension);

            await using (Stream streamAsync = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                await using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await streamAsync.CopyToAsync(fileStream);
                }
            }

            return "/thumbnails/" + bookId + extension;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save thumbnail");
            return string.Empty;
        }
    }

    public async Task<string> SaveThumbnail(Stream imageStream, string fileExtension, Guid bookId)
    {
        try
        {
            Directory.CreateDirectory(PathProvider.ThumbnailsFolderPath);
            string path = Path.Combine(PathProvider.ThumbnailsFolderPath, bookId + fileExtension);
            await using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await imageStream.CopyToAsync(fileStream);
            }
            return "/thumbnails/" + bookId + fileExtension;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save thumbnail");
            return string.Empty;
        }
    }

    public Task DeleteThumbnail(string fileName)
    {
        fileName = fileName.Split("/").Last();
        string filePath = Path.Combine(PathProvider.ThumbnailsFolderPath, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
        return Task.CompletedTask;
    }
}