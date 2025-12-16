using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class DeleteBookAdapter : IDeleteBookPort
{
    private readonly ApplicationDbContext _context;
    
    public Task DeleteBook(Book bookToRemove)
    {
        _context.Books.Remove(bookToRemove);
        return _context.SaveChangesAsync();
    }
}