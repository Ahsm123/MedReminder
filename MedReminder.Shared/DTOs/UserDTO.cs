using System.ComponentModel.DataAnnotations;

namespace MedReminder.Shared.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Indtast fornavn")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Indtast efternavn")]
    public string LastName { get; set; }
    public string Email { get; set; }
    [Required(ErrorMessage = "Telefonnummer skal være 8 karakterer")]
    [MinLength(8), MaxLength(8)]
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
}
