using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;

namespace BaseKnowledgeApp.Services
{
    public class PostgreSqlDemoServevice : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
