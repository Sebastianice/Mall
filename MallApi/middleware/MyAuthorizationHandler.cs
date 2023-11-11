using MallInfrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MallApi.middleware
{
    public class MyAuthorizationHandler : AuthorizationHandler<MyAuthorizationRequirement>
    {


        private readonly IHttpContextAccessor httpContext;
        private readonly MallContext dbcontext;
        public MyAuthorizationHandler(MallContext dbcontext, IHttpContextAccessor httpContext)
        {

            this.httpContext = httpContext;
            this.dbcontext = dbcontext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAuthorizationRequirement requirement)
        {
            string token = httpContext.HttpContext!.Request.Headers["Authorization"]!;

            if (context.User.IsInRole("Admin"))
            {
                if (requirement.Role == "Admin")
                {
                    var admToken = await dbcontext
                           .AdminUserTokens
                           .SingleOrDefaultAsync(s => s.Token == token);


                    if (admToken != null)
                    {
                        context.Succeed(requirement);
                        return;
                    }


                }
            }
            else if (context.User.IsInRole("User"))
            {
                if (requirement.Role == "User")
                {
                    var userToken = await dbcontext
                           .UserTokens
                           .SingleOrDefaultAsync(s => s.Token == token);

                    if (userToken != null)
                    {
                        context.Succeed(requirement);
                        return;
                    }




                }
            }

            context.Fail();
            return;




        }
    }
}
