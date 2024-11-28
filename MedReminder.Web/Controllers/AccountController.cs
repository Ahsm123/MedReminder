using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedReminder.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly RestClient _restClient;

        public AccountController()
        {
            _restClient = new RestClient("https://localhost:7023/api/v1/");
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

            var request = new RestRequest("Auth/Register", Method.Post);
            request.AddJsonBody(registerDTO);

            var response = await _restClient.ExecuteAsync(request);

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

            var request = new RestRequest("Auth/Login", Method.Post);
            request.AddJsonBody(loginDTO);

            var response = await _restClient.ExecuteAsync<LoginResponseDTO>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content);
                return View(loginDTO);
            }

            var loginResponse = response.Data;

            Response.Cookies.Append("JwtToken", loginResponse.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            });


            var userId = ExtractUserIdFromToken(loginResponse.Token);
            return RedirectToAction("Index", "User", new { id = userId });
        }

        private string ExtractUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim?.Value ?? string.Empty;
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }


    }
}
