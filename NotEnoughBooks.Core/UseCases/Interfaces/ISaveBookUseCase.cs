using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface ISaveBookUseCase
{
    bool Execute(Book book, IdentityUser user);
}