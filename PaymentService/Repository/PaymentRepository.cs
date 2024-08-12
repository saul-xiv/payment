using Microsoft.EntityFrameworkCore;
using PaymentCodeChallenge.Data;
using PaymentCodeChallenge.Models;

namespace PaymentCodeChallenge.Repository;

public class PaymentRepository(PaymentDbContext dbContext) : IPaymentRepository
{
    private readonly PaymentDbContext _dbContext = dbContext;
    private readonly DbSet<Payment> _payments = dbContext.Payments;
    public async Task<Payment> CreatePayment(Payment payment)
    {
        try
        {
            payment.IdUsuarioPaga = payment.UsuarioPaga.IdUsuario;
            payment.IdUsuarioRecibe = payment.UsuarioRecibe.IdUsuario;
            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Log error
            return null;
        }
    }

    public async Task<bool> UpdatePaymentStatus(Payment payment)
    {
        try
        {
            _dbContext.Payments.Update(payment);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Log error
            return false;
        }
    }

    public async Task<Payment> GetPayment(Guid id)
    {
        try
        {
            var payments = await _dbContext.Payments.ToListAsync();
            return await _dbContext.Payments.FirstOrDefaultAsync(p => p.IdPago == id)!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Log error
            return null;
        }
    }
}