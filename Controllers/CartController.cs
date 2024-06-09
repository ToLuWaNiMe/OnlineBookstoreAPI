using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Repositories;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireLoggedIn")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(int userId)
        {
            var cartItems = await _cartRepository.GetCartItems(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<ActionResult> AddCartItem([FromBody] CartItem cartItem)
        {
            await _cartRepository.AddCartItem(cartItem);
            return Ok("Cart Item Added Successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCartItem([FromBody] CartItem cartItem)
        {
            await _cartRepository.UpdateCartItem(cartItem);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{userId}/{bookId}")]
        public async Task<ActionResult> RemoveCartItem(int userId, int bookId)
        {
            await _cartRepository.RemoveCartItem(userId, bookId);
            return Ok("Item Removed");
        }

        [HttpDelete("{userId}/clear")]
        public async Task<ActionResult> ClearCart(int userId)
        {
            await _cartRepository.ClearCart(userId);
            return Ok();
        }
    }
}
