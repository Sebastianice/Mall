

using Mall.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class EfCoreServiceCollectionExtensions
{
    /// <summary>
    /// 添加仓储设置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MallContext>(options =>
        {

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            
        });

        return services;
    }

  
}
