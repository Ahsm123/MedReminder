using MedReminder.Dal.Models;

namespace MedReminder.Api.Services.Interfaces;

public interface ISubscriptionService
{
    Task AddSubscriptionAsync(Subscription subscription);
}
