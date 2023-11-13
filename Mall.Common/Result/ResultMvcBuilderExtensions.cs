using Mall.Common.Result;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;


public static class ResultMvcBuilderExtensions
{
    public static IMvcBuilder AddAppResult(this IMvcBuilder builder, Action<ResultOptions>? setupAction = null)
    {

        builder.AddMvcOptions(opt =>
        {
            opt.Filters.Add<ResultActionFilter>();
        });

        //默认配置
        builder.Services.AddTransient<IConfigureOptions<ResultOptions>, ResultOptionsSetup>();

        // 如果有自定义配置
        if (setupAction != null) builder.Services.Configure(setupAction);

        return builder;


    }
}

