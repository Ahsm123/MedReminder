namespace MedReminder.Dal.Models;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string SubscriptionType { get; set; }
    public DateTime SubscribedAt { get; set; }
    public bool IsActive { get; set; }
}
