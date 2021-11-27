using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.BackgroundTask
{
     public class Background : IHostedService
    {
        private readonly ILogger<Background> logger;
        private readonly IBettingApi bettingApi;

        public Background(ILogger<Background> logger,
            IBettingApi bettingApi)
        {
            this.logger = logger;
            this.bettingApi = bettingApi;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await bettingApi.GetBetsAll(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Printing worker stopping");
            return Task.CompletedTask;
        }
    }
}