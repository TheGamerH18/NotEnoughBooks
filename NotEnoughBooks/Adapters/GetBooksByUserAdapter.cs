using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class GetBooksByUserAdapter : IGetBooksByUserPort
{
    private readonly ApplicationDbContext _context;
    
    public IEnumerable<Book> GetBooks(IdentityUser user)
    {
        return _context.Books.Where(book => book.OwnedBy == user).OrderBy(book => book.AddedOn).ToArray();
    }
}