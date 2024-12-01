namespace MedReminder.Api.Services.Interfaces;

public interface ISmsService
{
    Task SendSmsAsync(string phoneNumber, string message);
}
