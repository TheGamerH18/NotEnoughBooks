using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface ISaveBookPort
{
    public Task SaveBook(Book book);
    Task SaveChanges();
}