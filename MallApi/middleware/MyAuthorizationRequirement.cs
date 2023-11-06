using Microsoft.AspNetCore.Authorization;

namespace MallApi.middleware
{
    public class MyAuthorizationRequirement : IAuthorizationRequirement
    {
       public string? Role { get; }
        public MyAuthorizationRequirement(string role)
        {
            Role = role;
        }

        public MyAuthorizationRequirement()
        {
        }
    }
}
