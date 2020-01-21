using System;
using System.Collections.Generic;
using System.Text;
using ConsoleDemo.DemoServices;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ConsoleDemo.Domain.AppOptions;

namespace ConsoleDemo.DemoServices
{
    class ChildCommonService : CommonService
    {
        private MySnapshotOptions _options { get; set; }
        ILogger _logger;
        public ChildCommonService(IOptionsSnapshot<MySnapshotOptions> options, ILogger<CommonService> logger):base(options, logger)
        {

        }

    }
}
