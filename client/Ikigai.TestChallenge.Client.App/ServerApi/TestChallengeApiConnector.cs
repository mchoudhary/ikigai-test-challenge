using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ikigai.TestChallenge.Client.App.ServerApi.Entites;
using Newtonsoft.Json;

namespace Ikigai.TestChallenge.Client.App.ServerApi
{
    public static class TestChallengeApiConnector
    {
        /// <summary>
        /// Gets days from cycle low -> halving -> cycle top in different bull runs
        /// </summary>
        /// <returns>Analysis model for days from cycle low -> halving -> cycle top in different bull runs</returns>
        public static async Task<DaysFromBtcHalvingsAnalysisModel> GetDaysFromBtcHalvingsAnalysisModel()
        {
            string apiUrl = ConfigurationManager.AppSettings["ServerApiUrl"];
            string endpoint = ConfigurationManager.AppSettings["DaysFromBtcHalvingsAnalysisEndpoint"];

            string strAnalysisModel = JsonConvert.DeserializeObject<object>(await GetRequest(apiUrl: apiUrl, endpoint: endpoint)).ToString();
            return JsonConvert.DeserializeObject<DaysFromBtcHalvingsAnalysisModel>(strAnalysisModel);
        }

        /// <summary>
        /// Gets Historical OHLCV Data for a given Ticker and Exchange
        /// </summary>
        /// <param name="ticker">Name of ticker</param>
        /// <param name="exchange">Name of exchange</param>
        /// <returns>List of Historical Daily OHLCV data</returns>
        public static async Task<List<OhlcvTick>> GetBistampHistoricalDailyOhlcvData(string ticker, string exchange)
        {
            string apiUrl = ConfigurationManager.AppSettings["ServerApiUrl"];
            string endpoint = ConfigurationManager.AppSettings["BistampHistoricalDailyOhlcvDataEndpoint"];
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["exchange"] = exchange;
            parameters["ticker"] = ticker;

            string ohlcvData = await GetRequest(apiUrl: apiUrl, endpoint: endpoint, parameters: parameters);
            var lines = ohlcvData.Split("\\n").Skip(1);

            List<OhlcvTick> timeseries = ohlcvData.Split("\\n").Skip(1).Take(lines.Count() - 1)
                                                  .Select(line => line.Split(','))
                                                  .Select(x => new OhlcvTick
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

        private static async Task<dynamic> GetRequest(string apiUrl, string endpoint, Dictionary<string, string> parameters = null)
        {
            dynamic response = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string queryParams = parameters != null ? ("?" + string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))) : string.Empty;


                HttpResponseMessage httpResp = await client.GetAsync($"{endpoint}{queryParams}");

                if (httpResp.IsSuccessStatusCode)
                    response = httpResp.Content.ReadAsStringAsync().Result;
            }

            return response;
        }
    }
}
