using LoginService.DTOs;

namespace LoginService.Services;

public interface ILogService
{
    Task<string> login(LoginDTO login);
}