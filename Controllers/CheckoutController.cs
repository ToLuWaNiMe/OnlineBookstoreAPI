using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Repositories;
using OnlineBookstore.Services;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireLoggedIn")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;

        public CheckoutController(ICartRepository cartRepository, IOrderRepository orderRepository, IPaymentService paymentService)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.BookPrices == null || !request.BookPrices.Any())
            {
                return BadRequest("Book prices must be provided.");
            }

            var cartItems = await _cartRepository.GetCartItems(request.UserId);
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

            var totalAmount = cartItems.Sum(item => item.Quantity * request.BookPrices[item.BookId]);
            var paymentSuccessful = await _paymentService.ProcessPayment(request.PaymentMethod, totalAmount);

            if (!paymentSuccessful)
            {
                return BadRequest("Payment failed.");
            }

            var order = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                PaymentMethod = request.PaymentMethod.ToString()
            };

            var orderItems = cartItems.Select(item => new OrderItem
            {
                BookId = item.BookId,
                Quantity = item.Quantity,
                Price = request.BookPrices[item.BookId]
            }).ToList();

            var orderId = await _orderRepository.CreateOrder(order, orderItems);

            await _cartRepository.ClearCart(request.UserId);

            return Ok(new { OrderId = orderId });
        }
    }
}
