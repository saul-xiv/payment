using PaymentCodeChallenge.Models;

namespace PaymentCodeChallenge.Repository;

public interface IPaymentRepository
{
    public Task<Payment> CreatePayment(Payment payment);
    Task<bool> UpdatePaymentStatus(Payment payment);
    Task<Payment> GetPayment(Guid id);
}