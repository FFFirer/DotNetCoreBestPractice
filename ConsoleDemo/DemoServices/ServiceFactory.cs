using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleDemo.DemoServices
{
    public class ServiceFactory : IServiceFactory
    {
        IHost _host;
        ILogger _logger;
        public ServiceFactory(IHost host, ILogger<ServiceFactory> logger)
        {
            _host = host;
            _logger = logger;
        }

        ICommonService IServiceFactory.Create(string className)
        {
            switch (className)
            {
                case "CommonService":
                    _logger.LogWarning($"Create Class {className}");
                    return _host.Services.GetService<ICommonService>();
                default:
                    return null;
            }
        }
    }
}
