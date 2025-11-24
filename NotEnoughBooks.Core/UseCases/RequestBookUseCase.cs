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
    private readonly IGetBookByQueryPort _getBookByQueryPort;
    private readonly ILogger<RequestBookUseCase> _logger;
    
    public async Task<BookResult> Execute(string query)
    {
        BookResult book = await _getBookByQueryPort.GetBook(query);
        return book;
    }
}