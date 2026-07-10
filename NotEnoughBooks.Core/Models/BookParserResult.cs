namespace NotEnoughBooks.Core.Models;

#nullable disable
public class BookParserResult
{
    public bool Success { get; private init; }

    public string ErrorMessage { get; private init; }

    public Book Book { get; private init; }

    public string[] ImageUrls { get; set; }

    public static BookParserResult Create(Book book, string[] imageUrls)
    {
        return new BookParserResult
        {
            Success = true,
            Book = book,
            ImageUrls = imageUrls
        };
    }

    public static BookParserResult Create(string errorMessage)
    {
        return new BookParserResult
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}