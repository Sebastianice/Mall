using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mall.Common.Configuration;
using Mall.Common.Startup;
using Microsoft.AspNetCore.Builder;
using NLog.Web;

namespace Microsoft.Extensions.DependencyInjection;

public static class SimpleWebApplicationBuilderExtensions
{
    public static WebApplicationBuilder SimpleConfigure(this WebApplicationBuilder builder)
    {
        // 配置
        var configuration = builder.Configuration;

        AppSettings.Configure(configuration);

        // 日志
        //builder.Logging.ClearProviders(); // .AddConsole()
        builder.Host.UseNLog();

        // 添加 HostedService 对静态 Helper 进行配置
        builder.Services.AddHostedService<SimpleHostedService>();


        return builder;
    }
}