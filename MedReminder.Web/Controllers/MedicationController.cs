using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedReminder.Web.Controllers
{
    public class MedicationController : Controller
    {
        private readonly RestClient _restClient;

        public MedicationController()
        {
            _restClient = new RestClient("https://localhost:7023/api/v1/");
        }

        public IActionResult Create(int userId)
        {
            if (userId == 0)
            {
                var token = Request.Cookies["JwtToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    userId = ExtractUserIdFromToken(token);
                }
            }

            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine($"Create GET: Received userId = {userId}");
            return View(new MedicationDTO { UserId = userId });
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
                return View(medicationDTO); //TODO: Fix det her
            }

            return RedirectToAction("Index", "User", new { userId = medicationDTO.UserId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            var token = Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new RestRequest($"Medications/{id}", Method.Delete);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content ?? "Failed to delete medication.");
            }

            return RedirectToAction("Index", "User", new { userId });
        }



        private int ExtractUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new RestRequest($"Medications/{id}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await _restClient.ExecuteAsync<MedicationDTO>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound("Medication not found.");
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicationDTO medication)
        {
            var token = Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(medication);
            }

            var request = new RestRequest($"Medications/{medication.Id}", Method.Put);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(medication);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Content ?? "Failed to update medication.");
                return View(medication);
            }

            return RedirectToAction("Index", "User", new { userId = medication.UserId });



        }
    }
}
