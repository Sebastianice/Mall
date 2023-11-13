using Mall.Common.Components.Authonrization;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class AuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(builder =>
            {
                builder.AddPolicy("User", p =>
                {
                    p.AddAuthenticationSchemes("default");
                   // p.AddAuthenticationSchemes("UserScheme");
                    p.AddRequirements(new RoleRequirement("User"));
            //       p.RequireRole("User");
                });

                builder.AddPolicy("Admin", p =>
                {
                    p.AddAuthenticationSchemes("default");
                    // p.AddAuthenticationSchemes("AdminScheme");
                    p.AddRequirements(new RoleRequirement("Admin"));
                  ///  p.RequireRole("Admin");
                });
            });


            services.AddTransient<IAuthorizationHandler, MallAuthorizationHandler>();


            return services;
        }
    }

}