using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Extensions;

public static class OrderBooksByExtensions
{
    public static string IsActive(this OrderBooksBy orderBy, OrderBooksBy check)
    {
        return orderBy == check ? "table-active" : string.Empty;
    }
}