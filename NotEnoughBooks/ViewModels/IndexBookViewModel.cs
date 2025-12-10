using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class IndexBookViewModel
{
    public IndexBookViewModel()
    {
        
    }
    
    private IndexBookViewModel(string searchText, IEnumerable<Book> books, OrderBooksBy order)
    {
        SearchText = searchText;
        Books = books;
        Order = order;
    }
    
    public string SearchText { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public OrderBooksBy Order { get; set; } = OrderBooksBy.Title;
    public bool OrderAsc { get; set; } = true;
    
    public static IndexBookViewModel Create(IEnumerable<Book> books, OrderBooksBy order = OrderBooksBy.Title, string searchText = "")
    {
        return new IndexBookViewModel(searchText, books, order);
    }
}