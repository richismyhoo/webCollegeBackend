using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using webCollege.DTOs;
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
            var (success, message, userId) = await _authService.Register(request.Email, request.Password, request.Name);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message, userId });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequestDto request)
        {
            var success = await _authService.VerifyEmailAsync(request.UserId, request.Code);
            if (!success) return BadRequest(new { message = "Неверный код или срок действия кода истек" });
            return Ok(new { message = "Email подтвержден" });
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
        
        [Required, MinLength(8)]
        public string Name { get; set; }
    }

    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
    
}