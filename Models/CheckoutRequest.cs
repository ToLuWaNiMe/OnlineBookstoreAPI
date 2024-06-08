using OnlineBookstore.Models.Enums;

namespace OnlineBookstore.Models
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Dictionary<int, decimal> BookPrices { get; set; }
    }
}
