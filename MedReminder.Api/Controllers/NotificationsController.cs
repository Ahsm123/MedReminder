using MedReminder.Api.DTOs;
using MedReminder.Api.Services.Interfaces;
using MedReminder.Dal.Models;
using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ISmsService _smsService;
        private readonly ISubscriptionService _subscriptionService;

        public NotificationsController(ISmsService smsService, ISubscriptionService subscriptionService)
        {
            _smsService = smsService;
            _subscriptionService = subscriptionService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SmsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.To) || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Phone number and message are required");
            }

            await _smsService.SendSmsAsync(request.To, request.Message);
            return Ok("Sms sent successfully");
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PhoneNumber) || request.UserId <= 0)
            {
                return BadRequest("Phone number and UserId are required.");
            }

            try
            {
                var subscription = new Subscription
                {
                    UserId = request.UserId,
                    PhoneNumber = request.PhoneNumber,
                    SubscriptionType = request.SubscriptionType ?? "MedicationReminder",
                    SubscribedAt = DateTime.Now,
                    IsActive = true
                };
                await _subscriptionService.AddSubscriptionAsync(subscription);

                await _smsService.SendSmsAsync(request.PhoneNumber, "Test SMS: You have subscribed to Medication Reminders!");

                return Ok("Subscription successful and test SMS sent.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}


