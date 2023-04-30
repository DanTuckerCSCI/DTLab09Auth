using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DTLab09Auth.Services
{
    public interface IRoleRepository
    {
        IQueryable<IdentityRole> ReadAll();
    }
}
