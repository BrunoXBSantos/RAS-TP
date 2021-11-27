using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Constants;

namespace API.RequestHandlers
{
    public class HttpClientRequestHandler: IRequestHandler
    {
        public string Get(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Add(BettingApiConstants.UserAgent, BettingApiConstants.UserAgentValue);

            //return await httpClient.GetStreamAsync(url);
            var response = httpClient.GetStringAsync(new Uri(url)).Result;
            return response;
        }
    }
}