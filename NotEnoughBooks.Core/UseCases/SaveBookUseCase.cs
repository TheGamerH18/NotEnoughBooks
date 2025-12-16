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
                await _saveBookPort.SaveBook(newBook);
            }
            else
            {
                Book oldBook = await _getBookByIdPort.GetBookById(newBook.Id);
                if (oldBook == null || oldBook.OwnedBy != user)
                    return false;

                // Image changed, delete old one and download new
                if (newBook.ImagePath != oldBook.ImagePath)
                {
                    await _cacheThumbnailPort.DeleteThumbnail(oldBook.ImagePath);
                    oldBook.ImagePath = await _cacheThumbnailPort.SaveThumbnail(newBook.ImagePath, newBook.Id);
                }
                oldBook.Authors = newBook.Authors;
                oldBook.Description = newBook.Description;
                oldBook.Isbn = newBook.Isbn;
                oldBook.PageCount = newBook.PageCount;
                oldBook.Price = newBook.Price;
                oldBook.Published = newBook.Published;
                oldBook.Publisher = newBook.Publisher;
                oldBook.Subtitle = newBook.Subtitle;
                oldBook.Title = newBook.Title;

                await _saveBookPort.SaveChanges();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}