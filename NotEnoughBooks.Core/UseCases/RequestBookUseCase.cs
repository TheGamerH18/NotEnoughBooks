using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class RequestBookUseCase : IRequestBookUseCase
{
    private readonly IGetBookByIsbnPort _getBookByIsbnPort;
    private readonly ILogger<RequestBookUseCase> _logger;
    
    public async Task<BookResult> Execute(string isbn)
    {
        BookResult book = await _getBookByIsbnPort.GetBook(isbn);
        return book;
    }
}