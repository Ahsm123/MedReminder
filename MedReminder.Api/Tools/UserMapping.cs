using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Tools;

public static class UserMapping
{
    public static User ToDomain(this UserDTO userDTO)
    {
        var nameParts = userDTO.Name.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        return new User
        {
            Id = userDTO.Id,
            FirstName = nameParts.Length > 0 ? nameParts[0] : string.Empty,
            LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty,
            Email = userDTO.Email,
            PasswordHash = userDTO.PasswordHash,
            CreatedAt = userDTO.CreatedAt
        };
    }
    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}".Trim(),
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CreatedAt = user.CreatedAt
        };
    }
}
