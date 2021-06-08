using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings
{
    public partial class DaysFromBtcHalvingChartControl : UserControl
    {
        public string SeriesTitle
        {
            set => txtTitle.Text = value;
        }

        public int xAxisRange
        {
            set
            {
                customXAxisDays_BullRun.MinimumValue = -1 * value;
                customXAxisDays_BullRun.MaximumValue = value;
            }
        }

        public string SeriesTag
        {
            set
            {
                chart_BullRun.Tag = value;
                lineBullRun.Tag = value; 
            }

        }


        public List<ohlcv_tick> OhlcData
        {
            set
            {
                lineBullRun.ItemsSource = value;
                customYAxisPrice_BullRun.MinimumValue = (double)value.Min(t => t.close);
            }
        }
    
        public DaysFromBtcHalvingChartControl()
        {
            InitializeComponent();
        }
    }
}
