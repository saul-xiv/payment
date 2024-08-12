using PaymentCodeChallenge.Models;

namespace PaymentCodeChallenge.Repository;

public interface IUserRepository
{
    Task<User> GetUser(string paymentDtoUsuarioPaga);
    Task<User> GetUserById(Guid paymentIdUsuarioPaga);
}