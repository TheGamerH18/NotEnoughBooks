using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class SaveBookAdapter : ISaveBookPort
{
    private readonly ApplicationDbContext _dbContext;
    
    public async Task SaveBook(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}