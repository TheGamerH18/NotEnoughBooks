using Microsoft.AspNetCore.Identity;

namespace NotEnoughBooks.Core.Models;
#nullable enable
public class Book
{
    public Guid Id { get; set; }
    
    public required string Isbn { get; set; }
    
    public required string Title { get; set; }
    
    public string? Subtitle { get; set; }
    
    public string? Description { get; set; }

    public string? Authors { get; set; }
    
    public string? Publisher { get; set; }
    
    //TODO: Proably use a nice default cover
    public string ImagePath { get; set; } = string.Empty;
    
    public int PageCount { get; set; }
    
    public double Price { get; set; }
    
    public DateOnly Published { get; set; }

    public DateTime AddedOn { get; set; } = DateTime.Now;
    
    public IdentityUser? OwnedBy { get; set; }
}