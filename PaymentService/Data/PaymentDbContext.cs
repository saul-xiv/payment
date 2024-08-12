using Microsoft.EntityFrameworkCore;
using PaymentCodeChallenge.Enums;
using PaymentCodeChallenge.Models;

namespace PaymentCodeChallenge.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RolCatalog> Roles { get; set; }
    public DbSet<EstadoPagoCatalog> EstadosPago { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolCatalog>(entity =>
        {
            entity.ToTable("catRol");
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<RolCatalog>().HasData(
            Enum.GetValues(typeof(Rol))
                .Cast<Rol>()
                .Select(e => new RolCatalog
                {
                    Id = (int)e,
                    Nombre = e.ToString()
                })
        );
        modelBuilder.Entity<EstadoPagoCatalog>(entity =>
        {
            entity.ToTable("catEstadoPago");
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<EstadoPagoCatalog>().HasData(
            Enum.GetValues(typeof(EstadoPago))
                .Cast<EstadoPago>()
                .Select(e => new EstadoPagoCatalog
                {
                    Id = (int)e,
                    Nombre = e.ToString()
                })
        );
        
        modelBuilder.Entity<User>().ToTable("Usuario");
        modelBuilder.Entity<User>()
            .HasKey(u => u.IdUsuario);
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    IdUsuario = new Guid("17fa39bc-5552-4c5f-ae43-9cb8bf428cc4"),
                    Nombre = "Admin",
                    Correo = "admin@example.com",
                    Contrasenia = "adminpassword",
                    Rol = Rol.Admin
                },
                new User
                {
                    IdUsuario = new Guid("a786221c-5f34-45e4-a296-85ff88edfa73"),
                    Nombre = "John Doe",
                    Correo = "user1@example.com",
                    Contrasenia = "user1password",
                    Rol = Rol.User
                },
                new User
                {
                    IdUsuario = new Guid("3fbfdc32-c0ae-4921-95ac-429d7cbd99b3"),
                    Nombre = "Jane Doe",
                    Correo = "user2@example.com",
                    Contrasenia = "user2password",
                    Rol = Rol.User
                },
                new User
                {
                    IdUsuario = new Guid("6cbeba98-7b66-4121-95c6-da6865b7820f"),
                    Nombre = "John Smith",
                    Correo = "user3@example.com",
                    Contrasenia = "user3password",
                    Rol = Rol.User
                }
            );
        modelBuilder.Entity<User>()
            .HasOne<RolCatalog>()
            .WithMany()
            .HasForeignKey(u => u.IdRol)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        modelBuilder.Entity<Payment>().ToTable("Pago");
        modelBuilder.Entity<Payment>().HasKey(p => p.IdPago)
            .HasName("PK_Payments");
        modelBuilder.Entity<Payment>().Property(p => p.Concepto).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.CantidadProductos).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.MontoTotal).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.IdUsuarioPaga).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.IdUsuarioRecibe).IsRequired();

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.UsuarioPaga)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioPaga)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.UsuarioRecibe)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioRecibe)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Payment>()
            .HasOne<EstadoPagoCatalog>()
            .WithMany()
            .HasForeignKey(p => p.EstadoPagoId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}