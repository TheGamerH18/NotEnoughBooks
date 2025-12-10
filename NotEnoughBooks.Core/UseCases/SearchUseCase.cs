using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class SearchUseCase : ISearchUseCase
{
    private readonly ISearchBooksPort _searchBooksPort;
    
    public IEnumerable<Book> Execute(string query, IdentityUser user)
    {
        return _searchBooksPort.Search(query, user);
    }
}