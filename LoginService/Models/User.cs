using System.ComponentModel.DataAnnotations.Schema;
using LoginService.Enums;

namespace LoginService.Models;

public class User
{
    public Guid IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Contrasenia { get; set; } = null!;
    public int IdRol { get; set; }
    [NotMapped]
    public Rol Rol
    {
        get => (Rol)IdRol;
        set => IdRol = (int)value;
    }
}