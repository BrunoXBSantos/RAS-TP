using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Constants;
using API.Interfaces;
using API.RequestHandlers;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class BettingApi : IBettingApi
    {
        private readonly ILogger<BettingApi> _logger;
        // classe que contem as apostas 
        private readonly IBetsUpdated betsUpdated;
        private int number = 0;

        public BettingApi(ILogger<BettingApi> logger, IBetsUpdated betsUpdated)
        {
            _logger = logger;
            this.betsUpdated = betsUpdated;
        }

        public async Task GetBetsAll(CancellationToken cancellationToken)
        {
            IRequestHandler httpClientRequestHandler = new HttpClientRequestHandler();

            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"Worker printing number: {number}");

                var response = await httpClientRequestHandler.Get(BettingApiConstants.url);

                var r = response + "\n " + $"Worker printing number: {number}";
                this.betsUpdated.setBets(response);

                await Task.Delay(1000 * 10);

            }
        }
    }
}