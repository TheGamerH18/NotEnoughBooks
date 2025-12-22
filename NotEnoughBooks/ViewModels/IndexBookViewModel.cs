using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class IndexBookViewModel
{
    public IndexBookViewModel() { }

    private IndexBookViewModel(IEnumerable<Book> books,
                               OrderBooksBy order,
                               bool orderAsc,
                               string searchText,
                               ViewMode view)
    {
        Books = books;
        Order = order;
        OrderAsc = orderAsc;
        SearchText = searchText;
        View = view;
    }

    public string SearchText { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public OrderBooksBy Order { get; set; } = OrderBooksBy.Title;
    public bool OrderAsc { get; set; } = true;
    public ViewMode View { get; set; } = ViewMode.Grid;

    public static IndexBookViewModel Create(IEnumerable<Book> books,
                                            IndexBookViewModel viewModel)
    {
        return new IndexBookViewModel(books, viewModel.Order, viewModel.OrderAsc, viewModel.SearchText, viewModel.View);
    }

    public enum ViewMode
    {
        Grid,
        List
    }
}