using NotEnoughBooks.ViewModels;

namespace NotEnoughBooks.Extensions;

public static class ViewModeExtensions
{
    public static string IsActive(this IndexBookViewModel.ViewMode current, IndexBookViewModel.ViewMode check)
    {
        return current == check ? "active" : string.Empty;
    }
}