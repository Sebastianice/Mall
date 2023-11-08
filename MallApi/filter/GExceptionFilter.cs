using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MallApi.filter
{
    public class GExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GExceptionFilter> logger;

        public GExceptionFilter(ILogger<GExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {

            if (!context.ExceptionHandled)
            {

               
                context.Result = new ContentResult()
                {
                    StatusCode = 200,
                    ContentType = "application/json;charset=utf-8",


                    Content = JsonConvert.SerializeObject
                   (Result.FailWithDetailed("", context.Exception.Message),
                   new JsonSerializerSettings
                   {
                       ContractResolver = new CamelCasePropertyNamesContractResolver()
                   })
            };



            logger.LogDebug(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;

        }
            return Task.CompletedTask;
        }
}
}
