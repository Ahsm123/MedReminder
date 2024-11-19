using MedReminder.DAL.Models;

namespace MedReminder.Api.Interfaces;

public interface IUserService
{
	Task<User> GetByIdAsync(int id);
	Task<int> CreateAsync(User user);
}
