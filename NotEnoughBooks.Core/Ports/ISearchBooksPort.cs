using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface ISearchBooksPort
{
    public IEnumerable<Book> Search(string query, IdentityUser user);
}