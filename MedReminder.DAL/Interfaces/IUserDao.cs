using MedReminder.DAL.Models;

namespace MedReminder.Dal.Interfaces;

public interface IUserDao
{
	Task<User> GetByIdAsync(int id);
	Task<int> CreateAsync(User user);

}
