using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IGetBooksByUserPort
{
    public IEnumerable<Book> GetBooks(IdentityUser user);
}