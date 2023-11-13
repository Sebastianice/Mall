using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Mall.Common.Result
{
    internal class ResultActionFilter : IAsyncActionFilter
    {
        private readonly ResultOptions _options;
     
        public ResultActionFilter(IOptions<ResultOptions> options)
        {
            _options = options.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var actionContext = await next();

            // 如果已经设置了结果，则直接返回
            if (context.Result != null || actionContext.Result != null) return;

            if (actionContext.Exception is ResultException resultException)
            {
                // 如果是结果异常，处理成返回结果，并标记异常已处理
                actionContext.Result = _options.ResultFactory(resultException);
                actionContext.ExceptionHandled = true;
            }
        }
    }
}
