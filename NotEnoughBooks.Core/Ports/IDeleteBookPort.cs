using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IDeleteBookPort
{
    public Task DeleteBook(Book bookToRemove);
}