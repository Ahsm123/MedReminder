using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Tools;

public static class UserMapping
{
    public static User ToDomain(this UserDTO userDTO)
    {
        return new User
        {
            Id = userDTO.Id,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Email = userDTO.Email,
            PhoneNumber = userDTO.PhoneNumber,
            PasswordHash = userDTO.PasswordHash,
            CreatedAt = userDTO.CreatedAt
        };
    }
    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = user.PasswordHash,
            CreatedAt = user.CreatedAt
        };
    }
}
