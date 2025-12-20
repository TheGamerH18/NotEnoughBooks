using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface ISaveBookUseCase
{
    Task<bool> Execute(Book newBook, IdentityUser user, Stream coverFileStream = null, string fileExtension = null);
}