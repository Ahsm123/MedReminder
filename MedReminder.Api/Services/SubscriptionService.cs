using MedReminder.Api.Services.Interfaces;
using MedReminder.Dal.Interfaces;
using MedReminder.Dal.Models;

namespace MedReminder.Api.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionDao _subscriptionDao;
    public SubscriptionService(ISubscriptionDao subscriptionDao)
    {
        _subscriptionDao = subscriptionDao;
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _subscriptionDao.AddSubscriptionAsync(subscription);
    }
}
