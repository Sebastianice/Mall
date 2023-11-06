using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MallApi.filter
{
    public class GExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {

            if (!context.ExceptionHandled)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(Result.FailWithMessage(context.Exception.Message))
                };
                context.ExceptionHandled = true;

            }
            return Task.CompletedTask;
        }
    }
}
