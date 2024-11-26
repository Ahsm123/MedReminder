using MedReminder.DAL.Models;

namespace MedReminder.Dal.Interfaces;

public interface IUserDao
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task<int> CreateUserAsync(User user);

}
