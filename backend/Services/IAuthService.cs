using System.Security.Claims;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public interface IAuthService
{
    Task<(bool Success, string Token)> LoginAsync(string username, string password);
    Task<(bool Success, string Message)> ValidateUserAsync(ClaimsPrincipal user, out int userId);
    Task<(bool Success, string Message)> SignupAsync(string username, string password, string name, string email);
    Task<(bool Success, string Message)> CreateUserAsync(string username, string password, UserRole role, string name, string email);
}
