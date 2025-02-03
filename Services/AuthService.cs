using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using webCollege.Context;
using webCollege.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webCollege.Services;

public class AuthService
{
    private readonly ApplicationContext _context;
    private readonly EmailService _emailService;
    private readonly IConfiguration _config;

    public AuthService(ApplicationContext context, IConfiguration config, EmailService emailService)
    {
        _context = context;
        _config = config;
        _emailService = emailService;
    }

    public async Task<(bool Success, string Message, int? UserId)> Register(string email, string password, string name)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed == true))
            return (false, "Пользователь с таким email уже существует", null);
        var userToDelete =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsEmailConfirmed == false);
        if (await _context.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed == false))
        {
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Email = email,
            PasswordHash = hashedPassword,
            Name = name,
            IsEmailConfirmed = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var verificationCode = new VerificationCode
        {
            UserId = user.Id,
            Code = GenerateVerificationCode(),
            ExpirationTime = DateTime.UtcNow.AddMinutes(10)
        };

        _context.VerificationCodes.Add(verificationCode);
        await _context.SaveChangesAsync();

        await _emailService.SendVerificationCodeAsync(user.Email, verificationCode.Code);

        return (true, "Регистрация успешна! Код подтверждения отправлен на email.", user.Id);
    }

    public async Task<bool> VerifyEmailAsync(int userId, string code)
    {
        var verification = await _context.VerificationCodes
            .FirstOrDefaultAsync(vc => vc.UserId == userId && vc.Code == code && vc.ExpirationTime > DateTime.UtcNow);

        if (verification == null)
            return false;

        var user = await _context.Users.FindAsync(userId);
        user.IsEmailConfirmed = true;
        _context.VerificationCodes.Remove(verification);
        await _context.SaveChangesAsync();
        return true;
    }

    private string GenerateVerificationCode()
    {
        return new Random().Next(100000, 999999).ToString();
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Неверные данные");

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}