using OnlineBookstore.Models.Enums;

namespace OnlineBookstore.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentMethod paymentMethod, decimal amount);
    }
}
