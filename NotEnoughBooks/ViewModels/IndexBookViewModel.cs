using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class IndexBookViewModel
{
    public IndexBookViewModel()
    {
        
    }
    
    private IndexBookViewModel(IEnumerable<Book> books, OrderBooksBy order, bool orderAsc, string searchText)
    {
        Books = books;
        Order = order;
        OrderAsc = orderAsc;
        SearchText = searchText;
    }
    
    public string SearchText { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public OrderBooksBy Order { get; set; }
    public bool OrderAsc { get; set; }
    
    public static IndexBookViewModel Create(IEnumerable<Book> books, OrderBooksBy order, bool orderAsc, string searchText = "")
    {
        return new IndexBookViewModel(books, order, orderAsc, searchText);
    }
}