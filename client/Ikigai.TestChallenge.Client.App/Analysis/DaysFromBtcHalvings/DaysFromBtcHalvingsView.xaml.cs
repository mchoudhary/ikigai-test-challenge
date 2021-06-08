using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings
{
    public partial class DaysFromBtcHalvingsView : UserControl
    {
        private DaysFromBtcHalvingsViewModel viewModel { get => DataContext as DaysFromBtcHalvingsViewModel; }

        public DaysFromBtcHalvingsView()
        {
            InitializeComponent();
            DataContext = new DaysFromBtcHalvingsViewModel();
            Loaded += (s, e) => _= DaysFromBtcHalvingsView_LoadedAsync();
        }

        private async Task DaysFromBtcHalvingsView_LoadedAsync()
        {
            await viewModel.InitializeAsync();
            DaysFromBtcHalvingsAnalysisModel model = viewModel.DataModel as DaysFromBtcHalvingsAnalysisModel;
            listOfInsights.ItemsSource = model.insights;

            for (int i = 0; i < model.bull_cycles.Count; i++)
            {
                bull_cycle cycle = model.bull_cycles[i];

                int minVal = cycle.cycle_bottom_days_from_halving_actual;
                int maxVal = Math.Max(cycle.cycle_top_days_from_halving_actual ?? 0, cycle.cycle_top_days_from_halving_projected ?? 0);

                int xAxisRangeNum = (int)(Math.Round((decimal)Math.Max(Math.Abs(minVal), maxVal) / 60) * 60) + 60;

                DaysFromBtcHalvingChartControl chartControl = new DaysFromBtcHalvingChartControl()
                {
                    SeriesTitle = $"{cycle.name} [Bottom: {cycle.cycle_bottom_days_from_halving_actual}d | Top: {(cycle.is_current ? cycle.cycle_top_days_from_halving_projected : cycle.cycle_top_days_from_halving_actual)}d{ (cycle.is_current ? $"(Projected)" : "")}]",
                    xAxisRange = xAxisRangeNum,
                    OhlcData = cycle.ohlcv_data,
                    SeriesTag = cycle.id
                };

                Grid.SetColumn(chartControl, i);

                if (i == 0)
                    chartControl.Margin = new System.Windows.Thickness(9);

                LayoutRoot.Children.Add(chartControl);
            }

            loadingScreen.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
