using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class SaveBookUseCase : ISaveBookUseCase
{
    private readonly ISaveBookPort _saveBookPort;
    private readonly ICacheThumbnailPort _cacheThumbnailPort;
    
    public async Task<bool> Execute(Book book, IdentityUser user)
    {
        try
        {
            book.Id = Guid.NewGuid();
            book.OwnedBy = user;
            book.AddedOn = DateTime.Now;
            book.ImagePath = await _cacheThumbnailPort.SaveThumbnail(book.ImagePath, book.Id);
            await _saveBookPort.SaveBook(book);
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}