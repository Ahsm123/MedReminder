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

            try
            {
                await _apiClient.PostAsync<object>("Auth/Register", registerDTO);

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerDTO);
            }
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

            try
            {
                var response = await _apiClient.PostAsync<LoginResponseDTO>("Auth/Login", loginDTO);

                _cookieService.SetJwtToken(response.Token, TimeSpan.FromMinutes(5));
                _cookieService.SetRefreshToken(response.RefreshToken, TimeSpan.FromMinutes(30));

                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginDTO);
            }
        }




        public IActionResult Logout()
        {
            _cookieService.DeleteJwtToken();
            return RedirectToAction("Login");
        }


    }
}
