using Microsoft.AspNetCore.Identity;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface IDeleteBookUseCase
{
    Task<bool> Execute(Guid bookId, IdentityUser user);
}