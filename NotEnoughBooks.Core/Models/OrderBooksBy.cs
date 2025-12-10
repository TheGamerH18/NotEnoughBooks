using System.ComponentModel;

namespace NotEnoughBooks.Core.Models;

public enum OrderBooksBy
{
    [Description("Added Date")]
    AddDate,
    [Description("Price")]
    Price,
    [Description("Pages")]
    PageCount,
    [Description("Release Date")]
    ReleaseDate,
    [Description("Title")]
    Title
}