using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class RequestNewBookUseCase : IRequestNewBookUseCase
{
    private readonly IGetBookByIsbnPort _getBookByIsbnPort;
    
    public async Task<BookParserResult> Execute(string isbn)
    {
        BookParserResult bookParser = await _getBookByIsbnPort.GetBook(isbn);
        return bookParser;
    }
}