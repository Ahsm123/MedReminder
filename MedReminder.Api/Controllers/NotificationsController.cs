using MedReminder.Api.DTOs;
using MedReminder.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public NotificationsController(ISmsService smsService)
        {
            _smsService = smsService;
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
    }
}


