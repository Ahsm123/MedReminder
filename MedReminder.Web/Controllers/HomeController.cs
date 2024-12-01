using MedReminder.ApiClient;
using MedReminder.Web.Models;
using MedReminder.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedReminder.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiClient _apiClient;
        private readonly ICookieService _cookieService;

        public HomeController(ILogger<HomeController> logger, IApiClient apiClient, ICookieService cookieService)
        {
            _logger = logger;
            _apiClient = apiClient;
            _cookieService = cookieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
