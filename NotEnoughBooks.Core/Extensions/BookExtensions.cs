using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Extensions;

public static class BookExtensions
{
    public static IEnumerable<Book> OrderBooksBy(this IEnumerable<Book> books, OrderBooksBy order, bool asc)
    {
        switch (order)
        {
            case Models.OrderBooksBy.AddDate:
                return books.OrderAscOrDesc(x => x.AddedOn, asc);
            case Models.OrderBooksBy.Price:
                return books.OrderAscOrDesc(x => x.Price, asc);
            case Models.OrderBooksBy.PageCount:
                return books.OrderAscOrDesc(x => x.PageCount, asc);
            case Models.OrderBooksBy.ReleaseDate:
                return books.OrderAscOrDesc(x => x.Published, asc);
            case Models.OrderBooksBy.Title:
                return books.OrderAscOrDesc(x => x.Title, asc);
            default:
                throw new ArgumentOutOfRangeException(nameof(order), order, null);
        }
    }

    private static IEnumerable<T> OrderAscOrDesc<T, TKey>(this IEnumerable<T> books, Func<T, TKey> selector, bool asc)
    {
        if (asc)
            return books.OrderBy(selector);
        return books.OrderByDescending(selector);
    }
}