using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class GetBookUseCase : IGetBookUseCase
{
    private readonly IGetBookByIdPort _bookByIdPort;
    
    public async Task<BookResult> Execute(Guid bookId, IdentityUser user)
    {
        Book bookById = await _bookByIdPort.GetBookById(bookId);
        if (bookById is not null && bookById.OwnedBy == user)
            return BookResult.Create(bookById);
        return BookResult.Create("No Books found");
    }
}