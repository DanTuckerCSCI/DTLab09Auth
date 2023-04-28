using System.ComponentModel;

namespace DTLab09Auth.Models
{
    public class UserListVM
    {
        public string Email { get; set; }
        public string Username { get; set; }
        [DisplayName("Number of roles")]
        public int NumberOfRoles { get; set; }
        [DisplayName("Role names")]
        public string RoleNames { get; set; }
    }
}