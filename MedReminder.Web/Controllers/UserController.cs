using MedReminder.ApiClient;
using MedReminder.Shared.DTOs;
using MedReminder.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Web.Controllers;

public class UserController : Controller
{
    private readonly IApiClient _apiClient;
    private readonly ICookieService _cookieService;

    public UserController(IApiClient apiClient, ICookieService cookieService)
    {
        _apiClient = apiClient;
        _cookieService = cookieService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _cookieService.ExstractUserIdFromToken();
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var token = _cookieService.GetJwtToken();
        var response = await _apiClient.GetAsync<List<MedicationDTO>>($"Medications/User/{userId}", token);
        return View(response.Data);
    }
}
