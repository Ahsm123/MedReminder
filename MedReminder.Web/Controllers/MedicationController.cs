using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MedReminder.Web.Controllers
{
    public class MedicationController : Controller
    {
        private readonly RestClient _restClient;

        public MedicationController()
        {
            _restClient = new RestClient("https://localhost:7023/api/v1/");
        }
        public async Task<IActionResult> Index(int userId)
        {
            var request = new RestRequest("Medications/{userId}", Method.Get);
            var response = await _restClient.ExecuteAsync<List<MedicationDTO>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content);
                return View();
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicationDTO medicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(medicationDTO);
            }

            var token = Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new RestRequest("Medications", Method.Post);
            request.AddJsonBody(medicationDTO);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content ?? "Failed to create medication.");
                return View(medicationDTO);
            }

            return RedirectToAction("Index", "User", new { id = medicationDTO.UserId });
        }
    }
}
