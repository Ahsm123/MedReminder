namespace MedReminder.Shared.DTOs;

public class SubscriptionRequest
{
    public int UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string SubscriptionType { get; set; }
}
