using DTLab09Auth.Models;
using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DTLab09Auth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IActionResult> AssignRole([Bind(Prefix = "Id")] string username)
        {
            var user = await _userRepository.ReadByUsernameAsync(username);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var allRoles = _roleRepository.ReadAll().ToList();
            var allRoleNames = allRoles.Select(r => r.Name);
            var rolesUserDoNotHave = allRoleNames.Except(user.Roles);
            ViewData["User"] = user;
            return View(rolesUserDoNotHave);
        }

        public async Task<IActionResult> ShowRoles(string userName)
        {
            ApplicationUser? user = await _userRepository.ReadAsync(userName);
            StringBuilder builder = new();
            foreach (var roleName in user!.Roles)
            {
                builder.Append(roleName + " ");
            }
            return Content($"UserName: {user.UserName} Roles: {builder}");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.ReadAllAsync();
            var userList = users
               .Select(u => new UserListVM
               {
                   Email = u.Email,
                   Username = u.UserName,
                   NumberOfRoles = u.Roles.Count,
                   RoleNames = string.Join(",", u.Roles.ToArray())
               });
            return View(userList);
        }

    }
}