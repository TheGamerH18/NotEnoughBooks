using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBookByIsbnPort
{
    public Task<BookParserResult> GetBook(string isbn);
}