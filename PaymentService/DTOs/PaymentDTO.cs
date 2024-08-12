using PaymentCodeChallenge.Enums;

namespace PaymentCodeChallenge.DTOs;

public record PaymentDTO : PaymentRequest
{
    public Guid IdPago { get; set; }
    public EstadoPago Estatus { get; set; }
    public static PaymentDTO Transform(PaymentRequest paymentRequest, EstadoPago status = EstadoPago.Registrado)
    {
        return new PaymentDTO
        {
            Concepto = paymentRequest.Concepto,
            CantidadProductos = paymentRequest.CantidadProductos,
            MontoTotal = paymentRequest.MontoTotal,
            UsuarioPaga = paymentRequest.UsuarioPaga,
            UsuarioRecibe = paymentRequest.UsuarioRecibe,
            Estatus = status
        };
    }
}