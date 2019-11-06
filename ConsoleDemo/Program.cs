using System;
using Microsoft.Extensions.Hosting;
using ConsoleDemo.DemoServices;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ConsoleDemo.Domain.AppOptions;
using ConsoleDemo.MainServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // 程序运行完成后可以直接释放退出程序
            using(var host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureAppConfiguration((hostcontext, appconfig)=> {
                    appconfig.AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);
                    appconfig.Build();
                })
                .ConfigureServices((hostcontext, services) =>
                {
                    // 注册Options
                    services.AddOptions();
                    //var config = new ConfigurationBuilder().AddJsonFile("appsetting.json", optional:true, reloadOnChange:true).Build();
                    var config = hostcontext.Configuration;
                    services.Configure<MyOptions>(config);
                    services.Configure<MySubOptions>(config.GetSection("SubOptions"));
                    services.Configure<MySnapshotOptions>(config.GetSection("SnapshotOptions"));
                    // 注册接口
                    services.AddScoped<IServiceFactory, ServiceFactory>();
                    services.AddScoped<ICommonService, CommonService>();
                    // 注册服务
                    services.AddHostedService<MyService>();
                    
                })
                .UseConsoleLifetime()
                .Build())
            {
                
                host.StartAsync();
            }
        }
    }
}
