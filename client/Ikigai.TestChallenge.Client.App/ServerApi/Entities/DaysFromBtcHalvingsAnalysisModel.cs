using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Ikigai.TestChallenge.Client.App.ServerApi.Entites
{
    public class DaysFromBtcHalvingsAnalysisModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bull_cycles")]
        public List<BullCycle> BullCycles { get; set; }

        [JsonProperty("analysed_at_utc")]
        public DateTime AnalysedAtUtc { get; set; }
    }

    public class BullCycle
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }

        [JsonProperty("ohlcv_data")]
        public List<OhlcvData> OhlcvData { get; set; }

        [JsonProperty("halving_date")]
        public HalvingDate HalvingDate { get; set; }

        [JsonProperty("cycle_bottom_days_from_halving_actual")]
        public int? CycleBottomDaysFromHalvingActual { get; set; }

        [JsonProperty("cycle_top_days_from_halving_projected")]
        public int? CycleTopDaysFromHalvingProjected { get; set; }

        [JsonProperty("cycle_top_days_from_halving_actual")]
        public int? CycleTopDaysFromHalvingActual { get; set; }
    }

    public class OhlcvData: OhlcvTick
    {
        [JsonProperty("days_from_halving")]
        public int DaysFromHalving { get; set; }

        [JsonProperty("is_halving_date")]
        public bool IsHalvingDate { get; set; }

        [JsonProperty("close_pct_change")]
        public decimal ClosePctChange { get; set; }
    }

    public class HalvingDate
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
