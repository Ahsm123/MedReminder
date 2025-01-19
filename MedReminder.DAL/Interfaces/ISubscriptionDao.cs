using MedReminder.Dal.Models;

namespace MedReminder.Dal.Interfaces
{
    public interface ISubscriptionDao
    {
        Task AddSubscriptionAsync(Subscription subscription);
    }
}
