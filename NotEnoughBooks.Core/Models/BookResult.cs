namespace NotEnoughBooks.Core.Models;

public class BookResult
{
    public bool Success { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public Book Book { get; set; }

    public static BookResult Create(Book book)
    {
        return new BookResult()
        {
            Success = true,
            Book = book
        };
    }

    public static BookResult Create(string message)
    {
        return new BookResult()
        {
            Success = false,
            ErrorMessage = message
        };
    }
}