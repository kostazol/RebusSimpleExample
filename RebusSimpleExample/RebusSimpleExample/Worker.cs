using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RebusSimpleExample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Send(new Message { Text = $"The time is {DateTimeOffset.Now}" });

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
