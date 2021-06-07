using Newtonsoft.Json;
using System;

namespace Ikigai.TestChallenge.Client.App.ServerApi.Entites
{
    public class OhlcvTick
    {
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }
    }
}
