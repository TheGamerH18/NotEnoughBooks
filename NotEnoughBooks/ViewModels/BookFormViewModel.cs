using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class BookFormViewModel
{
    public Book Book { get; set; }

    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    
    public IFormFile Image { get; set; }

    public static BookFormViewModel Create(Book book)
    {
        return new BookFormViewModel()
        {
            Book = book
        };
    }
    
    public static BookFormViewModel Create(Book book, string[] imageUrls)
    {
        return new BookFormViewModel()
        {
            Book = book,
            ImageUrls = imageUrls
        };
    }
}