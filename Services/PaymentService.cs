using OnlineBookstore.Models.Enums;

namespace OnlineBookstore.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ProcessPayment(PaymentMethod paymentMethod, decimal amount)
        {
            // Simulate payment processing based on the payment method
            switch (paymentMethod)
            {
                case PaymentMethod.Web:
                    return SimulateWebPayment(amount);
                case PaymentMethod.USSD:
                    return SimulateUSSDPayment(amount);
                case PaymentMethod.Transfer:
                    return SimulateTransferPayment(amount);
                default:
                    throw new ArgumentException("Invalid payment method");
            }
        }

        private Task<bool> SimulateWebPayment(decimal amount)
        {
            // Simulate web payment logic
            return Task.FromResult(true);
        }

        private Task<bool> SimulateUSSDPayment(decimal amount)
        {
            // Simulate USSD payment logic
            return Task.FromResult(true);
        }

        private Task<bool> SimulateTransferPayment(decimal amount)
        {
            // Simulate transfer payment logic
            return Task.FromResult(true);
        }
    }
}