using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.DataService
{
    public static class TimeseriesApiConnector
    {
        public static async Task<List<OhlcvData>> GetBistampHistoricalDailyOhlcvData(string ticker, string exchange)
        {
            string apiUrl = ConfigurationManager.AppSettings["TimeseriesApiUrl"];
            string endpoint = ConfigurationManager.AppSettings["BistampHistoricalDailyOhlcvDataEndpoint"];
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["exchange"] = exchange;
            parameters["ticker"] = ticker;

            string ohlcvData = await GetRequest(apiUrl: apiUrl, endpoint: endpoint, parameters: parameters);
            var lines = ohlcvData.Split("\\n").Skip(1);

            List<OhlcvData> timeseries = ohlcvData.Split("\\n").Skip(1).Take(lines.Count() - 1)
                                                  .Select(line => line.Split(','))
                                                  .Select(x => new OhlcvData
                                                  {
                                                      Date = DateTime.Parse(x[0]),
                                                      Open = decimal.Parse(x[1]),
                                                      High = decimal.Parse(x[2]),
                                                      Low = decimal.Parse(x[3]),
                                                      Close = decimal.Parse(x[4]),
                                                      Volume = decimal.Parse(x[5]),

                                                  }).ToList();

            return timeseries;
        }

        private static async Task<dynamic> GetRequest(string apiUrl, string endpoint, Dictionary<string, string> parameters)
        {
            dynamic response = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string queryParams = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));


                HttpResponseMessage httpResp = await client.GetAsync($"{endpoint}?{queryParams}");

                if (httpResp.IsSuccessStatusCode)
                    response = httpResp.Content.ReadAsStringAsync().Result;
            }

            return response;
        }
    }
}
