using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBookByIdPort
{
    public Task<Book> GetBookById(Guid bookId);
}