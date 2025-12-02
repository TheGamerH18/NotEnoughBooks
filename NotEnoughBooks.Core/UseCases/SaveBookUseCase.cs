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
    private readonly IGetBookByIdPort _getBookByIdPort;

    public async Task<bool> Execute(Book newBook, IdentityUser user)
    {
        try
        {
            newBook.OwnedBy = user;
            newBook.AddedOn = DateTime.Now;
            // Book is not in DB so fill in the model
            if (newBook.Id == Guid.Empty)
            {
                newBook.Id = Guid.NewGuid();
                newBook.ImagePath = await _cacheThumbnailPort.SaveThumbnail(newBook.ImagePath, newBook.Id);
            }
            else
            {
                Book oldBook = await _getBookByIdPort.GetBookById(newBook.Id);
                if (oldBook == null || oldBook.OwnedBy != user)
                    return false;

                newBook.AddedOn = DateTime.Now;
                // Image changed, delete old one and download new
                if (newBook.ImagePath != oldBook.ImagePath)
                {
                    await _cacheThumbnailPort.DeleteThumbnail(oldBook.ImagePath.Split("/").Last());
                    newBook.ImagePath = await _cacheThumbnailPort.SaveThumbnail(newBook.ImagePath, newBook.Id);
                }
            }

            await _saveBookPort.SaveBook(newBook);

            return true;
        }
        catch
        {
            return false;
        }
    }
}