using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services.Users;

namespace DTLab09Auth.Services
{
    public interface IUserRepository
    {
        //Add the ReadAllAsync to the user repository
        Task<ApplicationUser?> ReadByUsernameAsync(string username);
        async Task<IQueryable<ApplicationUser>> ReadAllAsync()
    
        {
        var users = _db.Users;
            //Read the roles for each user in the database
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return users;
        }
        Task<ApplicationUser?> ReadAsync(string username);

        Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);

        //Add AssignUserToRoleAsync
        Task AssignUserToRoleAsync(string userName, string roleName);
    }
}
