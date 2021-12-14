using System.Net.Http;
using System.Threading.Tasks;
using API.Model.Football;
using System.Text.Json;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
using System.Collections.Generic;
using API.Constants;

namespace API.RequestHandlers
{
    public class HttpClientRequestHandler: IRequestHandler
    {
        public async Task<ListEventAll> Get(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            // client.DefaultRequestHeaders.Accept.Add(
            //     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            // client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            client.DefaultRequestHeaders.Add(BettingApiConstants.UserAgent, BettingApiConstants.UserAgentValue);
            
            var streamTask = client.GetStreamAsync(url);
            ListEventAll events = await JsonSerializer.DeserializeAsync<ListEventAll>(await streamTask);

            return events;
        }
    }
}