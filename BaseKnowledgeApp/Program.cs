using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using System.IO;

namespace BaseKnowledgeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var host = CreateHost(args).Build())
            {
                host.StartAsync();
                host.WaitForShutdown();
            }
        }

        public static IHostBuilder CreateHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, app) =>
                { 

                })
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddNLog("NLog.config");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Services.LifetimeEventsHostedService>();
                    
                });
        }
    }
}
