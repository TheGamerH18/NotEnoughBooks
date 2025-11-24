using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface IGetBooksByUserUseCase
{
    public IEnumerable<Book> Execute(IdentityUser user);
}