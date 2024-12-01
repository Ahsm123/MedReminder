using MedReminder.Api.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MedReminder.Api.Services;

public class TwilioSmsService : ISmsService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhoneNumber;

    public TwilioSmsService(IConfiguration configuration)
    {
        _accountSid = configuration["Twilio:AccountSid"];
        _authToken = configuration["Twilio:AuthToken"];
        _fromPhoneNumber = configuration["Twilio:FromNumber"];
        TwilioClient.Init(_accountSid, _authToken);
    }
    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            await MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(phoneNumber),
                from: new Twilio.Types.PhoneNumber(_fromPhoneNumber),
                body: message
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending SMS: {ex.Message}");
            throw;
        }
    }
}
