using PaymentCodeChallenge.Enums;

namespace PaymentCodeChallenge.DTOs;

public record PaymentRequest
{
    public string Concepto { get; set; } = null!;
    public int CantidadProductos { get; set; }
    public decimal MontoTotal { get; set; }
    public string UsuarioPaga { get; set; } = null!;
    public string UsuarioRecibe { get; set; } = null!;
    
}