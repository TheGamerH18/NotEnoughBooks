namespace NotEnoughBooks.Core.Ports;

public interface ICacheThumbnailPort
{
    public Task<string> SaveThumbnail(string url, Guid bookId);
    public Task<string> SaveThumbnail(Stream imageStream, string fileExtension, Guid bookId);
    public Task DeleteThumbnail(string fileName);
}