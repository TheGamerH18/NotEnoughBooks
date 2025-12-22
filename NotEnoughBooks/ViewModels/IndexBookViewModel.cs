using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class IndexBookViewModel
{
    public IndexBookViewModel()
    { }

    private IndexBookViewModel(IEnumerable<Book> books,
                               OrderBooksBy order,
                               bool orderAsc,
                               string searchText,
                               ViewMode view,
                               int page,
                               int pageAmount)
    {
        Books = books;
        Order = order;
        OrderAsc = orderAsc;
        SearchText = searchText;
        View = view;
        Page = page;
        PageAmount = pageAmount;
    }

    public string SearchText { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public OrderBooksBy Order { get; set; } = OrderBooksBy.Title;
    public bool OrderAsc { get; set; } = true;
    public ViewMode View { get; set; } = ViewMode.Grid;
    public int Page { get; set; } = 1;
    public int PageAmount { get; set; }

    public static IndexBookViewModel Create(IEnumerable<Book> books,
                                            int pageAmount,
                                            IndexBookViewModel viewModel)
    {
        return new IndexBookViewModel(books,
                                      viewModel.Order,
                                      viewModel.OrderAsc,
                                      viewModel.SearchText,
                                      viewModel.View,
                                      viewModel.Page,
                                      pageAmount);
    }

    public enum ViewMode
    {
        Grid,
        List
    }
}