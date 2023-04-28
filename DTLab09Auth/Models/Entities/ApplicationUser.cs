using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTLab09Auth.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public ICollection<string> Roles { get; set; } = new List<string>();

        // roleName = "Admin"
        public bool HasRole(string roleName)
        {
            return Roles.Any(r => r == roleName);
        }
    }
}
