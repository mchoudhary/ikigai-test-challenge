using Ikigai.TestChallenge.Client.App.Common;
using Ikigai.TestChallenge.Client.App.ServerApi.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings
{
    public class DaysFromBtcHalvingsAnalysisModel : IAnalysisDataModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<bull_cycle> bull_cycles { get; set; }
        public metrics metrics { get; set; }
        public List<dynamic> ohlcv_data { get; set; }
        public DateTime analysed_at_utc { get; set; }

        public string GetChartDataForExcel()
        {
            throw new NotImplementedException();
        }

        public string GetChartDataForJson()
        {
            throw new NotImplementedException();
        }
    }

    public class halving_date
    {
        public DateTime date { get; set; }
        public string name { get; set; }
    }

    public class bull_cycle
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool is_current { get; set; }
        public halving_date halving_date { get; set; }
        public int cycle_bottom_days_from_halving_actual { get; set; }
        public int? cycle_top_days_from_halving_projected { get; set; }
        public int? cycle_top_days_from_halving_actual { get; set; }
        public List<ohlcv_tick> ohlcv_data { get; set; }
    }

    public class ohlcv_tick
    {
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }
        public decimal close_pct_change { get; set; }
        public int days_from_halving { get; set; }
        public bool is_halving_date { get; set; }
}

    public class metrics
    {
        public string cycle_where_projected_top_precede_actual { get; set; }
        public string projected_vs_actual_days_error_rate { get; set; }
    }
}
