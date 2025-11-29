namespace NotEnoughBooks.Core.Ports;

public interface ICacheThumbnailPort
{
    public Task<string> SaveThumbnail(string url, Guid bookId);
}