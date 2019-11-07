using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ConsoleDemo.DemoServices;
using ConsoleDemo.Domain.AppOptions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using NLog;

namespace ConsoleDemo.MainServices
{
    class MyService : IHostedService, IDisposable
    {
        private ICommonService _service;
        private IHostApplicationLifetime _applifetime;
        private MyOptions _option;
        private MySubOptions _suboption;
        private MySnapshotOptions _snapshotoption;
        private IServiceFactory _factory;
        private ILogger<MyService> _logger;
        public MyService(
            ICommonService commonService, 
            IHostApplicationLifetime hostApplicationLifetime, 
            IOptionsMonitor<MyOptions> options,
            IOptionsMonitor<MySubOptions> suboptions,
            IOptionsSnapshot<MySnapshotOptions> snapshotoptions,
            IServiceFactory factory,
            ILogger<MyService> logger)
        {
            _option = options.CurrentValue;
            _suboption = suboptions.CurrentValue;
            _snapshotoption = snapshotoptions.Value;
            _service = commonService;
            _applifetime = hostApplicationLifetime;
            _factory = factory;
            _logger = logger;
        }

        public void Dispose()
        {
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 注册Onstarted, OnStopping, OnStopped事件
            _applifetime.ApplicationStarted.Register(OnStarted);
            _applifetime.ApplicationStopping.Register(OnStopping);
            _applifetime.ApplicationStopped.Register(OnStopped);
            _service.TestConsole();
            LocalTest();
            _service.TestSnapshot();
            Thread.Sleep(20000);
            var service2 = _factory.Create("CommonService");
            service2.TestSnapshot();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_service.TestConsole("Stop Async!");

            _logger.LogInformation("$Stop Async");
            
            return Task.CompletedTask;
        }

        public void OnStarted()
        {
            //_service.TestConsole("App Started");
            _logger.LogInformation("App Started");
        }

        public void OnStopping()
        {
            //_service.TestConsole("App Stopping");
            _logger.LogInformation("App Stopping");
        }

        public void OnStopped()
        {
            //_service.TestConsole("App Stopped");
            _logger.LogInformation("App Stopped");
        }

        public void LocalTest()
        {
            //_service.TestConsole($"_option.Option1: {_option.Option1}");
            //_service.TestConsole($"_option.Option2: {_option.Option2}");
            //_service.TestConsole($"_suboption.suboption1: {_suboption.suboption1}");
            //_service.TestConsole($"_suboption.suboption2: {_suboption.suboption2}");
            //_service.TestConsole($"_snapshotoption.Snapshot1: {_snapshotoption.Snapshot1}");
            //_service.TestConsole($"_snapshotoption.Snapshot2: {_snapshotoption.Snapshot2}");
            _logger.LogInformation($"_option.Option1: {_option.Option1}");
            _logger.LogInformation($"_option.Option2: {_option.Option2}");
            _logger.LogInformation($"_suboption.suboption1: {_suboption.suboption1}");
            _logger.LogInformation($"_suboption.suboption2: {_suboption.suboption2}");
            _logger.LogInformation($"_snapshotoption.Snapshot1: {_snapshotoption.Snapshot1}");
            _logger.LogInformation($"_snapshotoption.Snapshot2: {_snapshotoption.Snapshot2}");

        }
    }
}
