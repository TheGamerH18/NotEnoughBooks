using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBookFromParserPort
{
    public Task<BookParserResult> GetBook(string isbn);
}