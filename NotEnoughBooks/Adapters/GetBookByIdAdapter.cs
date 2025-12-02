using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class GetBookByIdAdapter : IGetBookByIdPort
{
    private readonly ApplicationDbContext _context;
    
    public Task<Book> GetBookById(Guid bookId)
    {
        IQueryable<Book> queryable = _context.Books.Where(book => book.Id == bookId);
        if (queryable.Count() != 1)
            throw new Exception("Book not found");

        return Task.FromResult(queryable.First());
    }
}