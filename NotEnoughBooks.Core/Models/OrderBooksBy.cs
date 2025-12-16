using System.ComponentModel.DataAnnotations;

namespace NotEnoughBooks.Core.Models;

public enum OrderBooksBy
{
    [Display(Name = "Added Date")]
    AddDate,
    [Display(Name = "Price")]
    Price,
    [Display(Name = "Pages")]
    PageCount,
    [Display(Name = "Release Date")]
    ReleaseDate,
    [Display(Name = "Title")]
    Title
}