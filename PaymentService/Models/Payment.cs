using System.ComponentModel.DataAnnotations.Schema;
using PaymentCodeChallenge.DTOs;
using PaymentCodeChallenge.Enums;

namespace PaymentCodeChallenge.Models;

public class Payment
{
    public Guid IdPago { get; set; }
    public string Concepto { get; set; } = null!;
    public int CantidadProductos { get; set; }
    public decimal MontoTotal { get; set; }
    public User UsuarioPaga { get; set; } = null!;
    public Guid IdUsuarioPaga { get; set; }
    public User UsuarioRecibe { get; set; } = null!;
    public Guid IdUsuarioRecibe { get; set; }
    [NotMapped]
    public int EstadoPagoId { get; set; }
    [NotMapped]
    public EstadoPago Estatus
    {
        get => (EstadoPago)EstadoPagoId;
        set => EstadoPagoId = (int)value;
    }
    
    public static Payment TransformFromDTO(PaymentDTO paymentDTO, User usuarioPaga
    ,User usuarioRecibe)
    {
        return new Payment
        {
            IdPago = paymentDTO.IdPago == Guid.Empty ? Guid.NewGuid() : paymentDTO.IdPago,
            Concepto = paymentDTO.Concepto,
            CantidadProductos = paymentDTO.CantidadProductos,
            MontoTotal = paymentDTO.MontoTotal,
            UsuarioPaga = usuarioPaga,
            UsuarioRecibe = usuarioRecibe,
            Estatus = paymentDTO.Estatus
        };
    }

    public static PaymentDTO TransformToDTO(Payment payment)
    {
        return new PaymentDTO
        {
            IdPago = payment.IdPago,
            Concepto = payment.Concepto,
            CantidadProductos = payment.CantidadProductos,
            MontoTotal = payment.MontoTotal,
            UsuarioPaga = payment.UsuarioPaga.Nombre,
            UsuarioRecibe = payment.UsuarioRecibe.Nombre,
            Estatus = payment.Estatus
        };
    }
}