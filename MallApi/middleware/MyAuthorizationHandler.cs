
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace MallApi.middleware
{
    public class MyAuthorizationHandler : AuthorizationHandler<MyAuthorizationRequirement>
    {
        private readonly IMemoryCache cache;
        private readonly JwtSecurityTokenHandler securityToken;
        private readonly IHttpContextAccessor httpContext;

        public MyAuthorizationHandler(IMemoryCache cache, JwtSecurityTokenHandler securityToken, IHttpContextAccessor httpContext)
        {
            this.cache = cache;
            this.securityToken = securityToken;
            this.httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAuthorizationRequirement requirement)
        {
            var token = httpContext.HttpContext!.Request.Headers["Authorization"].ToString()[7..];

            string? sid = null;

            foreach (var item in context.User.Claims)
            {
                if (item.Type == JwtClaimTypes.Id)
                {
                    sid = item.Value;
                    break;
                }
            }


            if (context.User.IsInRole("Admin"))
            {
                if (requirement.Role == "Admin")
                {
                    if (cache.TryGetValue<string>($"Admin{sid}", out var v))
                    {
                        if (token == v)
                        {
                            //登出撤销已经签发的token
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }

                }
            }
            else if (context.User.IsInRole("User"))
            {
                if (requirement.Role == "User")
                {
                    if (cache.TryGetValue<string>($"User{sid}", out var v))
                    {
                        if (token == v)
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }

                }
            }

            context.Fail();
            return Task.CompletedTask;

        }
    }
}
