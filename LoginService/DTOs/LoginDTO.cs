namespace LoginService.DTOs;


public record LoginDTO : LoginRequest
{
    string token { get; set; }
    
    public static LoginDTO TransformToDTO(LoginRequest request)
    {
        return new LoginDTO
        {
            Username = request.Username,
            Password = request.Password
        };
    }
}