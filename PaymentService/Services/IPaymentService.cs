using PaymentCodeChallenge.DTOs;
using PaymentCodeChallenge.Enums;

namespace PaymentCodeChallenge.Services;

public interface IPaymentService
{
    public Task<PaymentDTO> CreatePayment(PaymentDTO paymentDto);
    Task<(bool, bool)> UpdatePaymentStatus(Guid id, EstadoPago status);
    Task<PaymentDTO> GetPayment(Guid id);
}