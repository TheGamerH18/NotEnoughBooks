using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBookByIsbnPort
{
    public Task<BookResult> GetBook(string isbn);
}