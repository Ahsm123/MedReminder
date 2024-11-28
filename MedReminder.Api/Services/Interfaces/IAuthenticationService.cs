using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Services.Interfaces;

public interface IAuthenticationService
{
    Task<User> RegisterAsync(RegisterDTO registerDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);

}
