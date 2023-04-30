using DTLab09Auth.Models;
using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace DTLab09Auth.Controllers
{
    [Authorize]
    public class HomeController : Controller
        
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly Random _random = new Random();
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application about page.";

            return View();
        }

        public async Task<IActionResult> GetUserId()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                var user = await _userRepo.ReadAsync(username);
                if (user != null)
                {
                    return Content($"{user.Id}");
                }
            }
            return Content("No User...");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult GetUserName()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                return Content(username);
            }
            return Content("No user");
        }

        public async Task<IActionResult> TestAssignUserToRole()
        {
            await _userRepo.AssignUserToRoleAsync("fake@email.com", "Admin");
            return Content("Assigned 'fake@email.com' to role 'Admin'");
        }


        public async Task<IActionResult> ShowRoles(string userName)
        {
            ApplicationUser? user = await _userRepo.ReadAsync(userName);
            StringBuilder builder = new();
            foreach (var roleName in user!.Roles)
            {
                builder.Append(roleName + " ");
            }
            return Content($"UserName: {user.UserName} Roles: {builder}");
        }

        [Authorize]
        public IActionResult Restricted()
        {
            return Content("This is restricted.");
        }

        [Authorize(Roles = "TestRole")]
        public IActionResult TestRoleCheck()
        {
            return Content("Hey you're a TestRole user!");
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