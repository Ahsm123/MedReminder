using MedReminder.ApiClient;
using MedReminder.Shared.DTOs;
using MedReminder.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiClient _apiClient;
        private readonly ICookieService _cookieService;

        public AccountController(IApiClient apiClient, ICookieService cookieService)
        {
            _apiClient = apiClient;
            _cookieService = cookieService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }

            var response = await _apiClient.PostAsync<RegisterDTO>("Auth/Register", registerDTO);


            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content);
                return View(registerDTO);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var response = await _apiClient.PostAsync<LoginResponseDTO>("Auth/Login", loginDTO);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content);
                return View(loginDTO);
            }

            _cookieService.SetJwtToken(response.Data.Token, TimeSpan.FromHours(1));
            return RedirectToAction("Index", "User");

        }


        public IActionResult Logout()
        {
            _cookieService.DeleteJwtToken();
            return RedirectToAction("Login");
        }


    }
}
