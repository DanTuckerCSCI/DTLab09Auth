using DTLab09Auth.Models;
using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace DTLab09Auth.Controllers
{
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

        public async Task<IActionResult> CreateTestUser()
        {
            var n = _random.Next(100);
            var check = await _userRepo.ReadAsync($"test{n}@test.com");
            if (check == null)
            {
                var user = new ApplicationUser
                {
                    Email = $"test{n}@test.com",
                    UserName = $"test{n}@test.com",
                    FirstName = $"user{n}",
                    LastName = $"Userlastname{n}"
                };
                await _userRepo.CreateAsync(user, "Pass1!");
                return Content($"Created test user 'test{n}@test.com' with a password 'Pass1!'");
            }
            return Content("The user was already created.");
        }

        public async Task<IActionResult> TestAssignUserToRole()
        {
            await _userRepo.AssignUserToRoleAsync("test@fakemail.com", "TestRole2");
            return Content("Assigned 'test@fakemail.com' to role 'TestRole2'");
        }

        public async Task<IActionResult> ShowRoles(string userName)
        {
            ApplicationUser? user = await _userRepo.ReadAsync(userName);
            string roles = String.Join(", ", user!.Roles);
            return Content($"Username: {user.UserName} Roles: {roles}");
        }

        public async Task<IActionResult> HasRole(string userName, string roleName)
        {
            var user = await _userRepo.ReadAsync(userName);
            if (user!.HasRole(roleName))
            {
                return Content($"{userName} has role {roleName}");
            }
            return Content($"{userName} does not have role {roleName}");
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