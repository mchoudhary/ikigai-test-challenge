using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.DataService
{
    class TimeseriesApiConnector
    {
        public async Task GetHistoricalDailyOhlcvDataAsync(string ticker, string exchange)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/ikigai-test-challenge/api/v1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var parameters = new Dictionary<string, string> { { "ticker", ticker }, { "exchange", exchange } };

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync($"timeseries/get-historical-daily-ohlcv-data?exchange={exchange}&ticker={ticker}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                }

            }
        }
    }
}
