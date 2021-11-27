using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using API.Constants;
using API.Interfaces;
using API.Model;
using API.RequestHandlers;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class GithubAPI : IGithubAPI
    {
        private readonly ILogger<GithubAPI> _logger;
        // classe que contem as apostas 
        private readonly IBetsUpdated betsUpdated;
        private int number = 0;

        public GithubAPI(ILogger<GithubAPI> logger, IBetsUpdated betsUpdated)
        {
            _logger = logger;
            this.betsUpdated = betsUpdated;
        }
        

        public async Task GettingAll(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"Worker printing number: {number}");
                await Task.Delay(1000 * 10);

                IRequestHandler httpClientRequestHandler = new HttpClientRequestHandler();

                var response = httpClientRequestHandler.Get(GithubConstants.url);

                var r = response + "\n " + $"Worker printing number: {number}";
                this.betsUpdated.setBets(r);

            }
        }
    }
}