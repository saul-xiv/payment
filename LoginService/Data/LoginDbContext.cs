using LoginService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Data;

public class LoginDbContext : DbContext
{
    public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Usuario");
        modelBuilder.Entity<User>()
            .HasKey(u => u.IdUsuario);
    }
}