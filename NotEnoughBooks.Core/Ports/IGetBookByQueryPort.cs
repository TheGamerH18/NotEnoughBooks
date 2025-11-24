using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBookByQueryPort
{
    public Task<BookResult> GetBook(string query);
}