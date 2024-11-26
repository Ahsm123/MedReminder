using MedReminder.Api.Interfaces;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;

namespace MedReminder.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public async Task<int> CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            return await _userDao.CreateUserAsync(user);
        }

        public Task<User> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            return _userDao.GetByIdAsync(id);
        }
    }
}
