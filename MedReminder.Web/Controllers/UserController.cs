using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedReminder.Web.Controllers;

public class UserController : Controller
{
    private readonly RestClient _restClient;

    public UserController()
    {
        _restClient = new RestClient("https://localhost:7023/api/v1/");
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? userId)
    {

        if (userId == null || userId == 0)
        {
            // Extract userId from JWT token
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                userId = ExtractUserIdFromToken(token);
            }
        }

        ViewData["UserId"] = userId;

        var request = new RestRequest($"Medications/User/{userId}", Method.Get);
        request.AddHeader("Authorization", $"Bearer {Request.Cookies["JwtToken"]}");

        var response = await _restClient.ExecuteAsync<List<MedicationDTO>>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            ModelState.AddModelError("", "Failed to load medications.");
            return View(new List<MedicationDTO>());
        }

        return View(response.Data);
    }

    private int? ExtractUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
    }
}
