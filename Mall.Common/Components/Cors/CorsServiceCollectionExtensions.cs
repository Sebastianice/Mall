using Mall.Common.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class CorsServiceCollectionExtensions
{
    /// <summary>
    /// 添加默认跨域配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleCors(this IServiceCollection services, Action<CorsOptions>? setupAction = null)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("local", policy =>
            {
                // 允许指定域名

                policy.WithOrigins(AppSettings.AllowCors)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();

            });
        });

        // 自定义配置
        if (setupAction != null)
        {
            services.Configure(setupAction);
        }

        return services;
    }
}
