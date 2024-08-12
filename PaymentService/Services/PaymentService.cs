using PaymentCodeChallenge.DTOs;
using PaymentCodeChallenge.Enums;
using PaymentCodeChallenge.Models;
using PaymentCodeChallenge.Repository;

namespace PaymentCodeChallenge.Services;

public class PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<PaymentDTO> CreatePayment(PaymentDTO paymentDto)
    {
        PaymentDTO paymentDtoRes;
        User usuarioPaga = await _userRepository.GetUser(paymentDto.UsuarioPaga);
        User usuarioRecibe = await _userRepository.GetUser(paymentDto.UsuarioRecibe);
        if(usuarioPaga == null || usuarioRecibe == null) return null;
        Payment payment = Payment.TransformFromDTO(paymentDto, usuarioPaga, usuarioRecibe);
        Payment objPayment = await _paymentRepository.CreatePayment(payment);
        paymentDtoRes = Payment.TransformToDTO(objPayment);
        return paymentDtoRes;
    }

    public async Task<(bool, bool)> UpdatePaymentStatus(Guid id, EstadoPago status)
    {
        (bool, bool) result = (false, false);
        Payment payment = await GetPaymentWithUsers(id);
        if(payment == null) return result;
        payment.Estatus = status;
        result.Item1 = true;
        result.Item2 = await _paymentRepository.UpdatePaymentStatus(payment);
        return result;
    }

    public async Task<PaymentDTO> GetPayment(Guid id)
    {
        Payment payment = await GetPaymentWithUsers(id);
        return payment != null ? Payment.TransformToDTO(payment) : null;
    }
    
    private async Task<Payment> GetPaymentWithUsers(Guid id)
    {
        Payment payment = await _paymentRepository.GetPayment(id);
        if(payment == null) return null;
        payment.UsuarioPaga = await _userRepository.GetUserById(payment.IdUsuarioPaga);
        payment.UsuarioRecibe = await _userRepository.GetUserById(payment.IdUsuarioRecibe);
        return payment;
    }
}