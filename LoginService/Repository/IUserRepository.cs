using LoginService.DTOs;
using LoginService.Models;

namespace LoginService.Repository;

public interface IUserRepository
{
    Task<User?> GetUser(LoginDTO login);
}