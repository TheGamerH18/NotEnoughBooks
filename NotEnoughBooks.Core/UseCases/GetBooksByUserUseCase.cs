using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class GetBooksByUserUseCase : IGetBooksByUserUseCase
{
    private readonly IGetBooksByUserPort _getBooksByUserPort;
    
    public IEnumerable<Book> Execute(IdentityUser user)
    {
        return _getBooksByUserPort.GetBooks(user);
    }
}