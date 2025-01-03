﻿using MedReminder.ApiClient;
using MedReminder.Shared.DTOs;
using MedReminder.Web.Filters;
using MedReminder.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Web.Controllers
{
    [RedirectUnauthorized]
    public class MedicationController : Controller
    {
        private readonly IApiClient _apiClient;
        private readonly ICookieService _cookieService;

        public MedicationController(IApiClient apiClient, ICookieService cookieService)
        {
            _apiClient = apiClient;
            _cookieService = cookieService;
        }

        public IActionResult Create(int userId)
        {
            userId = userId == 0 ? _cookieService.ExstractUserIdFromToken() ?? 0 : userId;

            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(new MedicationDTO { UserId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicationDTO medicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(medicationDTO);
            }

            var token = _cookieService.GetJwtToken();
            await _apiClient.PostAsync<object>("Medications", medicationDTO, token);
            return RedirectToAction("Index", "User", new { userId = medicationDTO.UserId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = _cookieService.GetJwtToken();
            await _apiClient.DeleteAsync($"Medications/{id}", token);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = _cookieService.GetJwtToken();
            var response = await _apiClient.GetAsync<MedicationDTO>($"Medications/{id}", token);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicationDTO medicationDTO)
        {
            var token = _cookieService.GetJwtToken();
            await _apiClient.PutAsync<MedicationDTO>($"Medications/{medicationDTO.Id}", medicationDTO, token);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> DailySchedule(DateTime? date)
        {
            var jwtToken = await EnsureJwtTokenAsync();

            var userId = _cookieService.ExstractUserIdFromToken();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var selectedDate = date ?? DateTime.Today;
            ViewBag.SelectedDate = selectedDate;

            var response = await _apiClient.GetAsync<List<MedicationDTO>>($"Medications/User/{userId}/schedule?date={selectedDate:yyyy-MM-dd}", jwtToken);

            if (response == null)
            {
                ModelState.AddModelError("", "Failed to load the daily schedule");
                return View(new List<MedicationDTO>());
            }

            return View(response);
        }

        private async Task<string> EnsureJwtTokenAsync()
        {
            var jwtToken = _cookieService.GetJwtToken();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                return jwtToken;
            }

            var refreshToken = _cookieService.GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Refresh the token
            var newTokens = await _apiClient.RefreshTokenAsync(refreshToken);

            _cookieService.SetJwtToken(newTokens.Token, TimeSpan.FromMinutes(5));
            _cookieService.SetRefreshToken(newTokens.RefreshToken, TimeSpan.FromMinutes(30));

            return newTokens.Token;
        }
    }
}
