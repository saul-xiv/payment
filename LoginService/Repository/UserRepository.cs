using LoginService.Data;
using LoginService.DTOs;
using LoginService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Repository;

public class UserRepository(LoginDbContext dbContext) : IUserRepository
{
    private readonly LoginDbContext _dbContext = dbContext;
    public async Task<User?> GetUser(LoginDTO login)
    {
        try
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Correo == login.Username && u.Contrasenia == login.Password);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}