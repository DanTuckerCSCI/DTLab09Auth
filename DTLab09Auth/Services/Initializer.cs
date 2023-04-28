using DTLab09Auth.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DTLab09Auth.Services
{
    public class Initializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Initializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedUsersAsync()
        {
            _db.Database.EnsureCreated();

            // adding roles to DB seed
            if (!_db.Roles.Any(r => r.Name == "Admin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            if (!_db.Roles.Any(r => r.Name == "Teacher"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Teacher" });

            if (!_db.Roles.Any(r => r.Name == "Student"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Student" });


            // add an admin user
            if (!_db.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@test.com",
                    UserName = "admin@test.com"
                };
                // create user
                await _userManager.CreateAsync(user, "Pass1!");
                // add to Admin role
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}