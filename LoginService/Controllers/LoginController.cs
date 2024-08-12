using LoginService.DTOs;
using LoginService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILogService logService) : ControllerBase
    {
        private readonly ILogService _logService = logService;
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            
            string token = await _logService.login(LoginDTO.TransformToDTO(request));
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
