using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace BaseKnowledgeApp.Services
{
    public class LifetimeEventsHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private System.Timers.Timer CustomTimer;

        public LifetimeEventsHostedService(
            ILogger<LifetimeEventsHostedService> logger,
            IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _appLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopped.Register(OnStopped);
            _appLifetime.ApplicationStopping.Register(OnStopping);

            _logger.LogInformation("Start Timer");

            Task.Factory.StartNew(() =>
            {
                CreateNewTimer(cancellationToken);
                CustomTimer.Start();
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Timer...");
            CustomTimer.Dispose();
            _logger.LogInformation("Stopped Timer...");
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        /// <param name="token"></param>
        private void CreateNewTimer(CancellationToken token)
        {
            CustomTimer = new System.Timers.Timer(5000);
            CustomTimer.Elapsed += (source, args) => DoWork(token);
            CustomTimer.AutoReset = true;
            CustomTimer.Enabled = true;
        }

        /// <summary>
        /// 创建计时器要使用的事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void TimerEvent(object source, System.Timers.ElapsedEventArgs args)
        {
            //DoWork();
            _logger.LogInformation("Timer work done");
        }

        public void DoWork(CancellationToken token)
        {
            int TotalCount = 10;
            CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(token);
            CancellationToken cancellationToken = source.Token;

            while(TotalCount>0)
            {
                token.ThrowIfCancellationRequested();
                TotalCount--;
                _logger.LogInformation($"Now is {TotalCount}");
                Thread.Sleep(1000);
            }
        }
    }
}
