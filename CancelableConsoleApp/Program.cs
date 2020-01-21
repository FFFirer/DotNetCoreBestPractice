using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CancelableConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // 配置通用主机
            using (var host = Host.CreateDefaultBuilder()
                .ConfigureLogging((logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((context, services)=> 
                {
                    services.AddScoped<IHostedService, TestService>();
                })
                .UseConsoleLifetime()
                .Build())
            {
                host.StartAsync();
                host.WaitForShutdown();
            }
        }
    }
}
