using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface ISearchUseCase
{
    IEnumerable<Book> Execute(string query, IdentityUser user);
}