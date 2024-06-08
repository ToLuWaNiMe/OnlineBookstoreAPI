using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Repositories;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public PurchaseHistoryController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetPurchaseHistory(int userId)
        {
            var orders = await _orderRepository.GetOrders(userId);
            return Ok(orders);
        }

        [HttpGet("{userId}/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderDetails(int userId, int orderId)
        {
            var orderItems = await _orderRepository.GetOrderItems(orderId);
            return Ok(orderItems);
        }
    }
}
