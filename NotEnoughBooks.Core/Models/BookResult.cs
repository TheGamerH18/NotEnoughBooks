namespace NotEnoughBooks.Core.Models;

public class BookResult
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public Book Book { get; set; }
    
    public string[] ImageUrls { get; set; }

    public static BookResult Create(Book book, string[] foundImageUrls = null)
    {
        return new BookResult()
        {
            Success = true,
            Book = book,
            ImageUrls = foundImageUrls ?? Array.Empty<string>()
        };
    }

    public static BookResult Create(string message)
    {
        return new BookResult()
        {
            Success = false,
            Message = message
        };
    }
}