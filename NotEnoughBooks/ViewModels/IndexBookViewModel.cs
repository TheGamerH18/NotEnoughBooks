using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class IndexBookViewModel
{
    public IndexBookViewModel()
    {
        
    }
    
    private IndexBookViewModel(string searchText, IEnumerable<Book> books)
    {
        SearchText = searchText;
        Books = books;
    }
    
    public string SearchText { get; set; }
    public IEnumerable<Book> Books { get; set; }

    public static IndexBookViewModel Create(string searchText, IEnumerable<Book> books)
    {
        return new IndexBookViewModel(searchText, books);
    }
}