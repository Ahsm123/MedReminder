using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Services.Interfaces;

public interface IAuthenticationService
{
    Task<User> RegisterAsync(RegisterDTO registerDTO);
    Task<(string JwtToken, string RefreshToken)> LoginWithRefreshTokenAsync(LoginDTO loginDTO);
    Task<(string JwtToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
}
