using Mall.Common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ResultOptionsSetup : IConfigureOptions<ResultOptions>
    {
        public void Configure(ResultOptions options)
        {
            // 默认结果工厂
            options.ResultFactory = result =>
            {
                return new ObjectResult(result.AppResult)
                {
                    StatusCode = 200
                };
            };
        }
    }
}