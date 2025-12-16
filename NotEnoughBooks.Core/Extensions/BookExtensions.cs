using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Extensions;

public static class BookExtensions
{
    public static IEnumerable<Book> OrderBooksBy(this IEnumerable<Book> books, OrderBooksBy order, bool asc)
    {
        return order switch
        {
            Models.OrderBooksBy.AddDate => books.OrderAscOrDesc(x => x.AddedOn, asc),
            Models.OrderBooksBy.Price => books.OrderAscOrDesc(x => x.Price, asc),
            Models.OrderBooksBy.PageCount => books.OrderAscOrDesc(x => x.PageCount, asc),
            Models.OrderBooksBy.ReleaseDate => books.OrderAscOrDesc(x => x.Published, asc),
            Models.OrderBooksBy.Title => books.OrderAscOrDesc(x => x.Title, asc),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };
    }

    private static IEnumerable<T> OrderAscOrDesc<T, TKey>(this IEnumerable<T> books, Func<T, TKey> selector, bool asc)
    {
        if (asc)
            return books.OrderBy(selector);
        return books.OrderByDescending(selector);
    }
}