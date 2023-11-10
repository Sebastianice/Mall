using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MallApi.middleware
{
    public class AuthenFailMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenFailMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            finally  
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                Code code = 0;

                if (statusCode == 401)
                {
                    code = Code.UNLOGIN;
                    msg = "未登录!";
                }
                else if (statusCode == 403)
                {
                    code = Code.ERROR;
                    msg = "无权访问";
                }
                if (code != 0)
                {
                    var result = JsonConvert.SerializeObject(
                 new Result()
                 {
                     ResultCode = code,
                     Message = msg,
                 }, new   JsonSerializerSettings
                 {
                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                 });
                    context.Response.ContentType = "application/json;charset=utf-8";
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(result);
                }
            }


            }
        }
        public static class AuthenFailMiddlewareExtensions
        {
            public static IApplicationBuilder UseAuthenFailHandling(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<AuthenFailMiddleware>();
            }
        }
    }
