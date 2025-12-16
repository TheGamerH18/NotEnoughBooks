using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class DeleteBookUseCase : IDeleteBookUseCase
{
    private readonly IGetBookByIdPort _getBookByIdPort;
    private readonly IDeleteBookPort _deleteBookPort;
    private readonly ICacheThumbnailPort _cacheThumbnailPort;

    public async Task<bool> Execute(Guid bookId, IdentityUser user)
    {
        Book bookById = await _getBookByIdPort.GetBookById(bookId);
        if (bookById == null || bookById.OwnedBy != user)
            return false;
        await _cacheThumbnailPort.DeleteThumbnail(bookById.ImagePath);
        await _deleteBookPort.DeleteBook(bookById);
        return true;
    }
}