using Dapper;
using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrder(Order order, IEnumerable<OrderItem> orderItems)
        {
            var queryOrder = "INSERT INTO Orders (User_Id, Order_Date, Total_Amount, Payment_Method) VALUES (@UserId, @OrderDate, @TotalAmount, @PaymentMethod) RETURNING Id";
            var queryOrderItem = "INSERT INTO Order_Items (Order_Id, Book_Id, Quantity, Price) VALUES (@OrderId, @BookId, @Quantity, @Price)";

            using (var connection = _context.CreateConnection())
            {
                var orderId = await connection.ExecuteScalarAsync<int>(queryOrder, order);

                foreach (var item in orderItems)
                {
                    item.OrderId = orderId;
                    await connection.ExecuteAsync(queryOrderItem, item); 
                }

                return orderId;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            var query = "SELECT * FROM Orders WHERE User_Id = @UserId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Order>(query, new { UserId = userId });
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems(int orderId)
        {
            var query = "SELECT * FROM Order_Items WHERE Order_Id = @OrderId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<OrderItem>(query, new { OrderId = orderId });
            }
        }
    }
}
