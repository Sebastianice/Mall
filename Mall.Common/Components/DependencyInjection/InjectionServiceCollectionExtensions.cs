using Mall.Common.Helpers;

namespace Microsoft.Extensions.DependencyInjection;

public static class InjectionServiceCollectionExtensions
{
    /// <summary>
    /// 自动注册程序集内以 Service 结尾的服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dllNames"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoServices(this IServiceCollection services, params string[] dllNames)
    {
        // 根据名称获取程序集
        var assemblies = AssemblyHelper.GetAssemblies(dllNames);

        // 获取程序集内名称以 Service 结尾的 class
        var serviceTypes = assemblies.SelectMany(a => a.GetTypes())
                                     .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract
                                     && !t.IsGenericType)
                                     .Distinct();

        // 遍历，将服务默认注册为瞬态服务（生命周期：Transient）
        foreach (var serviceType in serviceTypes)
        {

            // 注册所有实现的实例
            var serviceInterfaces = serviceType.GetInterfaces().FirstOrDefault();

            if (serviceInterfaces != null)
            {
                RegistrationType(services, serviceInterfaces, serviceType);

            }
            else
            {
                RegistrationType(services, serviceType, serviceType);

            }
        }

        return services;

        // AddAutoServices() 内部静态函数
        static void RegistrationType(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifecyleType = ServiceLifetime.Transient)
        {
            // 注册服务
            switch (lifecyleType)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(serviceType, implementationType);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(serviceType, implementationType);
                    break;
                case ServiceLifetime.Transient:
                default:
                    services.AddTransient(serviceType, implementationType);
                    break;
            }
        }
    }
}
