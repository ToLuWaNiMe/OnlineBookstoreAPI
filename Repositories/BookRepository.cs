using Dapper;
using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _context;

        public BookRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var query = "SELECT * FROM Books";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Book>(query);
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            var query = "SELECT * FROM Books WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Book>(query, new { Id = id });
            }
        }

        public async Task<int> AddBook(Book book)
        {
            var query = "INSERT INTO Books (Title, Genre, ISBN, Author, PublicationYear) VALUES (@Title, @Genre, @ISBN, @Author, @PublicationYear)";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, book);
            }
        }

        public async Task<int> UpdateBook(Book book)
        {
            var query = "UPDATE Books SET Title = @Title, Genre = @Genre, ISBN = @ISBN, Author = @Author, PublicationYear = @PublicationYear WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, book);
            }
        }

        public async Task<int> DeleteBook(int id)
        {
            var query = "DELETE FROM Books WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Book>> SearchBooks(string query)
        {
            var sqlQuery = "SELECT * FROM Books WHERE Title LIKE @Query OR Author LIKE @Query OR Genre LIKE @Query OR PublicationYear LIKE @Query";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Book>(sqlQuery, new { Query = $"%{query}%" });
            }
        }
    }
}
