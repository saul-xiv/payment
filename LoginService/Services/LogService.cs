using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginService.DTOs;
using LoginService.Models;
using LoginService.Repository;
using Microsoft.IdentityModel.Tokens;

namespace LoginService.Services;

public class LogService(IUserRepository userRepository, IConfiguration configuration) : ILogService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;
    public async Task<string> login(LoginDTO login)
    {
        User user = await _userRepository.GetUser(login);
        if (user == null)
            return null;
        string token = GenerateJwtToken(user);
        return token;
    }
    
    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Correo),
            new Claim("role", user.Rol.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}