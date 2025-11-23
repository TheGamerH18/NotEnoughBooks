using Microsoft.AspNetCore.Identity;
using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface IRequestBookUseCase
{
    Task<BookResult> Execute(string isbn);
}