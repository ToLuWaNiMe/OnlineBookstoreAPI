using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItems(int userId);
        Task<CartItem> GetCartItem(int userId, int bookId);
        Task<int> AddCartItem(CartItem cartItem);
        Task<int> UpdateCartItem(CartItem cartItem);
        Task<int> RemoveCartItem(int userId, int bookId);
        Task<int> ClearCart(int userId);
    }
}
