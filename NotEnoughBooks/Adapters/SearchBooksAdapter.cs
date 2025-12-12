using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;

namespace NotEnoughBooks.Adapters;

[GenerateFullConstructor]
public partial class SearchBooksAdapter : ISearchBooksPort
{
    private readonly ApplicationDbContext _dbContext;
    
    /// <inheritdoc />
    public IEnumerable<Book> Search(string query, IdentityUser user)
    {
        return _dbContext.Books.Where(b => b.Title.ToLower().Contains(query.ToLower()) && b.OwnedBy == user).ToArray();
    }
}