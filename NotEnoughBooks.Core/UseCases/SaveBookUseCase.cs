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
    
    public bool Execute(Book book, IdentityUser user)
    {
        try
        {
            book.OwnedBy = user;
            book.AddedOn = DateTime.Now;
            _saveBookPort.SaveBook(book);
            return true;
        }
        catch
        {
            return false;
        }
    }
}