using DTLab09Auth.Models.Entities;
using DTLab09Auth.Services;

namespace DTLab09Auth.Services
{
    public interface IUserRepository
    {
        //Add the ReadAllAsync to the user repository
        Task<ApplicationUser?> ReadByUsernameAsync(string username);
        Task<IQueryable<ApplicationUser>> ReadAllAsync();
        Task<ApplicationUser?> ReadAsync(string username);
        Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);

        //Add AssignUserToRoleAsync
        Task AssignUserToRoleAsync(string userName, string roleName);
    }

}