using Dapper;
using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext _context;

        public CartRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(int userId)
        {
            var query = "SELECT * FROM CartItems WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<CartItem>(query, new { UserId = userId });
            }
        }

        public async Task<CartItem> GetCartItem(int userId, int bookId)
        {
            var query = "SELECT * FROM CartItems WHERE UserId = @UserId AND BookId = @BookId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<CartItem>(query, new { UserId = userId, BookId = bookId });
            }
        }

        public async Task<int> AddCartItem(CartItem cartItem)
        {
            var query = "INSERT INTO CartItems (UserId, BookId, Quantity) VALUES (@UserId, @BookId, @Quantity)";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, cartItem);
            }
        }

        public async Task<int> UpdateCartItem(CartItem cartItem)
        {
            var query = "UPDATE CartItems SET Quantity = @Quantity WHERE UserId = @UserId AND BookId = @BookId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, cartItem);
            }
        }

        public async Task<int> RemoveCartItem(int userId, int bookId)
        {
            var query = "DELETE FROM CartItems WHERE UserId = @UserId AND BookId = @BookId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { UserId = userId, BookId = bookId });
            }
        }

        public async Task<int> ClearCart(int userId)
        {
            var query = "DELETE FROM CartItems WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { UserId = userId });
            }
        }
    }
}
