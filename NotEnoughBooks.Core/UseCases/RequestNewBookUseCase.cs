using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class RequestNewBookUseCase : IRequestNewBookUseCase
{
    private readonly IGetBookFromParserPort _getBookFromParserPort;
    
    public async Task<BookParserResult> Execute(string isbn)
    {
        BookParserResult bookParser = await _getBookFromParserPort.GetBook(isbn);
        return bookParser;
    }
}