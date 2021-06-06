using Newtonsoft.Json;
using System;

namespace Ikigai.TestChallenge.Client.App.ServerApi.Entites
{
    public class OhlcvTick
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }
    }
}
