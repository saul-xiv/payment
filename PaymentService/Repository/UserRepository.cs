using Microsoft.EntityFrameworkCore;
using PaymentCodeChallenge.Data;
using PaymentCodeChallenge.Models;
namespace PaymentCodeChallenge.Repository;

public class UserRepository(PaymentDbContext paymentDbContext) : IUserRepository
{
    private readonly PaymentDbContext _paymentDbContext = paymentDbContext;
    public async Task<User> GetUser(string nombreUsuario)
    {
        try
        {
            var result = await _paymentDbContext.Users.FirstOrDefaultAsync(u => u.Nombre.ToLower() == nombreUsuario.ToLower());
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<User> GetUserById(Guid idUsuario)
    {
        try
        {
            return await _paymentDbContext.Users.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario)!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}