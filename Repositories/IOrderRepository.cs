using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(Order order, IEnumerable<OrderItem> orderItems);
        Task<IEnumerable<Order>> GetOrders(int userId);
        Task<IEnumerable<OrderItem>> GetOrderItems(int orderId);
    }
}
