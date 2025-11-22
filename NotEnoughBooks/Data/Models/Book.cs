using Microsoft.AspNetCore.Identity;

namespace NotEnoughBooks.Data.Models;

public class Book
{
    public Guid Id { get; set; }
    
    public string Isbn { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public double Price { get; set; }
    
    public string ImagePath { get; set; }
    
    public string PublishedYear { get; set; }
    
    public DateTime AddedOn { get; set; }
    
    public IdentityUser OwnedBy { get; set; }
}