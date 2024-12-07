using MedReminder.ApiClient;
using MedReminder.Shared.DTOs;
using MedReminder.Web.Models;
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

    public async Task<IActionResult> Index()
    {
        try
        {
            var userId = _cookieService.ExstractUserIdFromToken();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var token = _cookieService.GetJwtToken();
            var medicationsDto = await _apiClient.GetAsync<List<MedicationDTO>>($"Medications/User/{userId}", token);

            var medicationsViewModel = medicationsDto.Select(m => new MedicationViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Dosage = m.Dosage,
                DosageUnit = m.DosageUnit,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Description = m.Description,
                Instructions = m.Instructions
            }).ToList();

            return View(medicationsViewModel);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(new List<MedicationViewModel>());
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            id = _cookieService.ExstractUserIdFromToken() ?? 0;
        }

        var token = _cookieService.GetJwtToken();
        var response = await _apiClient.GetAsync<UserDTO>($"Users/{id}", token);
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserDTO userDTO)
    {
        var token = _cookieService.GetJwtToken();
        await _apiClient.PutAsync<UserDTO>($"Users/{userDTO.Id}", userDTO, token);
        return RedirectToAction("Index", "User");
    }


}
