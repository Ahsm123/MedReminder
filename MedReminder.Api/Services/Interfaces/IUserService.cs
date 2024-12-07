using MedReminder.DAL.Models;

namespace MedReminder.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
        Task<bool> UpdateUserAsync(User user);
    }
}
