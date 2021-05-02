using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR
{
    public class TimeService : BackgroundService
    {
        private readonly IHubContext<TimeHub> hub;
        private readonly ILogger<TimeService> logger;

        public TimeService(IHubContext<TimeHub> hub, ILogger<TimeService> logger)
        {
            this.hub = hub;
            this.logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("Time Service is starting");

            stoppingToken.Register(() => { logger.LogDebug("Time Service is stopping. Cancellation was requested."); });

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogDebug("Time Service is sending time");

                var dateTime = DateTime.Now;
                await hub.Clients.All.SendAsync("currentTime", new TimeModel { Hours = dateTime.Hour, Minutes = dateTime.Minute, Seconds = dateTime.Second });

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            logger.LogDebug("Time Service is stopping");
        }
    }
}
