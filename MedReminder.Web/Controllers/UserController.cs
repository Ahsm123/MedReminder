using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MedReminder.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly RestClient _restClient;

        public UserController()
        {
            _restClient = new RestClient("https://localhost:7023/api/v1/");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["UserId"] = id;

            var token = Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new RestRequest($"Medications/{id}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _restClient.ExecuteAsync<List<MedicationDTO>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                ModelState.AddModelError("", "Failed to load medications.");
                return View(new List<MedicationDTO>());
            }

            return View(response.Data);
        }

    }
}
