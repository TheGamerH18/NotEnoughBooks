using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface IGetBookUseCase
{
    Task<BookResult> Execute(Guid bookId, IdentityUser user);
}