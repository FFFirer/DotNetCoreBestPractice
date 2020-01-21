using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CancelableConsoleApp
{
    public class TestService : IHostedService
    {
        private IHostApplicationLifetime _appLifeTime { get; set; }
        private ILogger<TestService> _logger { get; set; }
        public TestService(IHostApplicationLifetime appLifeTime, ILogger<TestService> logger)
        {
            _appLifeTime = appLifeTime;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //启动
            _appLifeTime.ApplicationStarted.Register(OnStarted);
            _appLifeTime.ApplicationStopped.Register(OnStoped);
            _appLifeTime.ApplicationStopping.Register(OnStoping);

            try
            {
                Task.Factory.StartNew(() =>
                {
                    Console.Write("loop count:");
                    int LoopCount = int.Parse(Console.ReadLine());
                    while (LoopCount > 0)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(cancellationToken);
                        }
                        _logger.LogInformation($"Leave{LoopCount}");
                        Thread.Sleep(1000);
                        LoopCount--;
                    }
                }, cancellationToken);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
            }
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //停止
            Console.WriteLine("bye bye~");
            return Task.CompletedTask;
        }

        public void OnStarted()
        {
            _logger.LogInformation("App Started!");
        }

        public void OnStoped()
        {
            _logger.LogError("App Stoped!");
        }

        public void OnStoping()
        {
            _logger.LogWarning("App is Stoping!");
        }
    }
}
