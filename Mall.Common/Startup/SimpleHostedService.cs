using Mall.Common.Components.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;



namespace Mall.Common.Startup
{
    internal class SimpleHostedService : IHostedService
    {
        public IHost Host { get; set; }

        public SimpleHostedService(IHost host)
        {
            Host = host;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceProvider = Host.Services;

            // Json 配置
            var jsonOptions = serviceProvider.GetService<IOptions<JsonOptions>>();
            if (jsonOptions != null) JsonHelper.Configure(jsonOptions!.Value.JsonSerializerOptions);


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
