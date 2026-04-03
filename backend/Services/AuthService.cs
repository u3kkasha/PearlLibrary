using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PearlLibrary.Data;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public class AuthService : IAuthService
{
    private readonly LibraryDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(LibraryDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private (string Hash, byte[] Salt) HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[16];
        rng.GetBytes(salt);

        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);

        return (Convert.ToBase64String(hash), salt);
    }

    private bool VerifyPassword(string password, string storedHash, byte[] salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(hash) == storedHash;
    }

    public async Task<(bool Success, string Token)> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !VerifyPassword(password, user.PasswordHash, user.Salt!))
        {
            return (false, string.Empty);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return (true, tokenString);
    }

    public Task<(bool Success, string Message)> ValidateUserAsync(ClaimsPrincipal user, out int userId)
    {
        var userIdClaim = user.FindFirst("UserId");
        userId = 0;

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId))
        {
            return Task.FromResult((false, "Unauthorized"));
        }

        return Task.FromResult((true, string.Empty));
    }

    public async Task<(bool Success, string Message)> SignupAsync(string username, string password, string name, string email)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
        {
            return (false, "Username already exists");
        }

        var (hash, salt) = HashPassword(password);
        var user = new User
        {
            Username = username,
            PasswordHash = hash,
            Salt = salt,
            Role = UserRole.Member,
            Name = name,
            Email = email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "Signup successful");
    }

    public async Task<(bool Success, string Message)> CreateUserAsync(string username, string password, UserRole role, string name, string email)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
        {
            return (false, "Username already exists");
        }

        var (hash, salt) = HashPassword(password);
        var user = new User
        {
            Username = username,
            PasswordHash = hash,
            Salt = salt,
            Role = role,
            Name = name,
            Email = email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "User created successfully");
    }
}
