using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace MallApi.filter {
    public class TokenFilter : ActionFilterAttribute {
        private readonly IMemoryCache cache;

        public TokenFilter(IMemoryCache cache) {
            this.cache = cache;
        }
       

        public override void OnActionExecuting([FromServices] ActionExecutingContext context) {
            base.OnActionExecuting(context);

            var token = context.HttpContext.Request.Headers["Authorization"].ToString()[7..];
            var tokenHandler = new JwtSecurityTokenHandler();
            var rt = tokenHandler.ReadJwtToken(token);
            var id = rt.Claims.Single(w => w.Type == JwtClaimTypes.Id);

            if (cache.TryGetValue<string>("user" + id.Value, out var value)) {

                if (token != value)
                    context.Result = new ContentResult {
                        Content = "未登录",
                        StatusCode = 401
                    };
            } else {
                context.Result = new ContentResult {
                    Content = "未登录",
                    StatusCode = 401
                };
            }

        }
    }
}
