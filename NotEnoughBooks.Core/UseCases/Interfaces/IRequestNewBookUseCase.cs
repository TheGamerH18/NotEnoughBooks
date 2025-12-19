using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface IRequestNewBookUseCase
{
    Task<BookParserResult> Execute(string isbn);
}