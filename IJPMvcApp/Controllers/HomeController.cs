using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userName = User.Identity.Name;
                string role = User.Claims.ToArray()[4].Value;
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/AuthSvc/") };
                string token = await client2.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpContext.Session.SetString("token", token);
            }
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
