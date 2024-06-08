using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<int> AddBook(Book book);
        Task<int> UpdateBook(Book book);
        Task<int> DeleteBook(int id);
        Task<IEnumerable<Book>> SearchBooks(string query);
    }
}
