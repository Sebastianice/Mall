using System.Collections.Immutable;
using IdentityModel;
using Mall.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Mall.Common.Components.Authonrization
{

    //需要查询数据库，只能放在接口层了
    public class MallAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly MallContext dbcontext;

        public MallAuthorizationHandler(MallContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext == null)
            {
                context.Fail();
                return;

            }

            var idClaim = context.User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Id);


            if (idClaim == null)
            {
                context.Fail();
                return;

            }
            var id = long.Parse(idClaim.Value);


            string token = httpContext.Request.Headers["Authorization"]!;

            if (context.User.IsInRole("Admin"))
            {
                if (requirement.Role == "Admin")
                {
                    var admToken = await dbcontext
                           .AdminUserTokens

                           .SingleOrDefaultAsync(a => a.AdminUserId == id);


                    if (admToken != null && admToken.Token == token)
                    {
                        context.Succeed(requirement);
                        return;
                    }


                }
            }
            if (context.User.IsInRole("User"))
            {

                if (requirement.Role == "User")
                {
                    var userToken = await dbcontext
                           .UserTokens
                            .SingleOrDefaultAsync(s => s.UserId == id);

                    if (userToken != null && userToken.Token == token)
                    {
                        context.Succeed(requirement);
                        return;
                    }




                }
            }


            context.Fail();
        }
    }
}
