using DTLab09Auth.Models.Entities;

namespace DTLab09Auth.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> ReadAsync(string username);

        Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);

        Task AssignUserToRoleAsync(string userName, string roleName);
    }
}
