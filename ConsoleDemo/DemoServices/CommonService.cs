using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using ConsoleDemo.Domain.AppOptions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ConsoleDemo.DemoServices
{
    class CommonService : ICommonService
    {
        private MySnapshotOptions _options { get; set; }
        ILogger _logger;
        public CommonService(IOptionsSnapshot<MySnapshotOptions> options, ILogger<CommonService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }
        public void TestConsole()
        {
            Console.WriteLine("Test Cosnole");
        }

        public void TestConsole(string msg)
        {
            Console.WriteLine($"Msg:{msg}");
        }

        public void TestSnapshot()
        {
            _logger.LogDebug($"_options.Snapshot1: {_options.Snapshot1}");
            _logger.LogError($"_options.Snapshot2: {_options.Snapshot2}");
        }
    }
}
