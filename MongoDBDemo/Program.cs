using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostcontext, appconfig) => {
                    appconfig.AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);
                    appconfig.Build();
                })
                .ConfigureServices((hostcontext, services) => {
                    services.AddLogging(logbuilder =>
                    {
                        logbuilder.SetMinimumLevel(LogLevel.Debug);
                    });

                    services.AddOptions();
                    services.AddHostedService<DemoService>();
                })
                .UseConsoleLifetime().Build())
            {
                host.StartAsync();
            }
        }
    }
}
