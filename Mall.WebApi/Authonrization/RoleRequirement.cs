using Microsoft.AspNetCore.Authorization;

namespace Mall.Common.Components.Authonrization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role;

        public RoleRequirement(string role)
        {
            this.Role = role;
        }
    }
}