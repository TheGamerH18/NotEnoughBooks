namespace NotEnoughBooks.Core.Models;

public class BookParserResult
{
    public bool Success { get; set; }

    public string ErrorMessage { get; set; }

    public Book Book { get; set; }

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