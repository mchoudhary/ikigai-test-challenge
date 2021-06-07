using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings
{
    public partial class DaysFromBtcHalvingsView : UserControl
    {
        DaysFromBtcHalvingsViewModel viewModel { get; }

        public DaysFromBtcHalvingsView()
        {
            InitializeComponent();
            viewModel = new DaysFromBtcHalvingsViewModel();
            Loaded += (s, e) => _= DaysFromBtcHalvingsView_LoadedAsync();
        }

        private async Task DaysFromBtcHalvingsView_LoadedAsync()
        {
            await viewModel.InitializeAsync();
            DaysFromBtcHalvingsAnalysisModel model = viewModel.DataModel as DaysFromBtcHalvingsAnalysisModel;

            for (int i = 0; i < model.bull_cycles.Count; i++)
            {
                bull_cycle cycle = model.bull_cycles[i];

                int minVal = cycle.cycle_bottom_days_from_halving_actual;
                int maxVal = Math.Max(cycle.cycle_top_days_from_halving_actual ?? 0, cycle.cycle_top_days_from_halving_projected ?? 0);

                int xAxisRangeNum = (int)(Math.Round((decimal)Math.Max(Math.Abs(minVal), maxVal) / 60) * 60) + 60;

                XamDataChart bullRunChart = new XamDataChart
                {
                    Name = $"chart_{cycle.id}",
                    Tag = $"chart_{cycle.id}",
                    Margin = new System.Windows.Thickness(6)
                };

                Grid.SetColumn(bullRunChart, i);

                NumericXAxis xAxis = new NumericXAxis
                {
                    MinimumValue = -1 * xAxisRangeNum,
                    MaximumValue = xAxisRangeNum,
                    Interval = 120
                };

                PercentChangeYAxis yAxis = new PercentChangeYAxis
                {
                    ScaleMode = NumericScaleMode.Logarithmic,
                    MinimumValue = 0
                };

                bullRunChart.Axes.Add(xAxis);
                bullRunChart.Axes.Add(yAxis);

                ScatterLineSeries lineSeries = new ScatterLineSeries
                {
                    ItemsSource = cycle.ohlcv_data,
                    Thickness = 1,
                    MarkerType = MarkerType.None,
                    Name = $"lineSeries_{cycle.id}",
                    Tag = cycle.id,
                    XAxis = xAxis,
                    YAxis = yAxis,
                    XMemberPath = "days_from_halving",
                    YMemberPath = "close_pct_change",
                    IsHighlightingEnabled = false
                };

                bullRunChart.Series.Add(lineSeries);
                LayoutRoot.Children.Add(bullRunChart);
            }
        }
    }
}
