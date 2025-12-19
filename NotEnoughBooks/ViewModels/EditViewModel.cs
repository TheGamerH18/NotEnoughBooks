using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.ViewModels;

public class EditViewModel
{
    public bool Success { get; set; }

    public string ErrorMessage { get; set; }

    public BookFormViewModel BookFormViewModel { get; set; }

    public static EditViewModel Create(BookResult bookResult)
    {
        return new EditViewModel()
        {
            Success = bookResult.Success,
            ErrorMessage = bookResult.ErrorMessage,
            BookFormViewModel = bookResult.Book == null ? null : BookFormViewModel.Create(bookResult.Book)
        };
    }
}