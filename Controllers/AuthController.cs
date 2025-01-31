using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;
using webCollege.Models;

namespace webCollege.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = new User { Email = request.Email };
                await _authService.Register(user, request.Password);
                return Ok(new { Message = "Успешная регистрация" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.Login(request.Email, request.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
    }
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, MinLength(8)]
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
    
}