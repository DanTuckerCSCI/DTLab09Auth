using DTLab09Auth.Models;
using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace DTLab09Auth.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepo.ReadAllAsync();
            var userList = users.Select(u => new UserListVM
            {
                Email = u.Email,
                Username = u.Username,
                NumberOfRoles = u.Roles.Count(),
                RoleNames = string.Join(", ", u.Roles.ToArray())
            });

            return View(userList);
        }
    }